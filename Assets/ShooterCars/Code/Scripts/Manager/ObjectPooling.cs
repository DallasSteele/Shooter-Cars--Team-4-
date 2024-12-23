using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ShooterCar.Manager
{
    public class ObjectPooling : MonoBehaviour
    {
        [Header("Enemy Properties")]
        /// <summary>
        /// Amount of enemies spawned once, object pooling pattern used
        /// </summary>
        [Tooltip("Amount of enemies spawned once")]
        [SerializeField] private int m_EnemiesAmount;
        /// <summary>
        /// Enemy object to duplicate and reused, object pooling pattern used
        /// </summary>
        [Tooltip("Enemy object to duplicate and reused")]
        [SerializeField] private GameObject m_EnemyPrefab;
        /// <summary>
        /// GameObject as parent to pool all enemies
        /// </summary>
        [Tooltip("GameObject as parent to object pool")]
        [SerializeField] private GameObject m_EnemyList;

        [Header("Projectile Properties")]
        /// <summary>
        /// Amount of bullets spawned once, object pooling pattern used
        /// </summary>
        [Tooltip("Amount of bullets spawned once")]
        [SerializeField] private int m_BulletAmount;
        /// <summary>
        /// Bullet object to duplicate and reused, object pooling pattern used
        /// </summary>
        [Tooltip("Bullet object to duplicate and reused")]
        [SerializeField] private GameObject m_BulletPrefab;
        /// <summary>
        /// GameObject as parent to pool all bullets
        /// </summary>
        [Tooltip("GameObject as parent to object pool")]
        [SerializeField] private GameObject m_BulletList;
        /// <summary>
        /// Explosion effect for Explode bullet type
        /// </summary>
        [Tooltip("Explosion effect for Explode bullet type")]
        [SerializeField] private GameObject m_ExplosionEffect;

        [SerializeField] private GameObject laserEffect;

        [Header("Looping Road Properties")]
        [SerializeField] private GameObject[] m_Roads;
        [SerializeField] private GameObject m_RoadList;

        /// <summary>
        /// Pool variable for enemy
        /// </summary>
        private Queue<GameObject> m_EnemiesPool = new Queue<GameObject>();
        /// <summary>
        /// Pool variable for bullet
        /// </summary>
        private Queue<GameObject> m_BulletPool = new Queue<GameObject>();
        /// <summary>
        /// Pool variable for explosion effect
        /// </summary>
        private Queue<GameObject> m_ExplodeEffect = new Queue<GameObject>();
        private Queue<GameObject> laserEffectPool = new Queue<GameObject>();
        private Queue<GameObject> m_RoadPool = new Queue<GameObject>();

        /// <summary>
        /// Single Instance for easy call this class from entire project
        /// </summary>
        public static ObjectPooling Instance { get; private set; }
        /// <summary>
        /// Amount of enemies spawned once, object pooling pattern used
        /// </summary>
        public float EnemiesAmount { get { return m_EnemiesAmount; } }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            foreach (var item in m_Roads)
            {
                CreatePool(item, m_RoadList, m_RoadPool);
            }
        }

        private void OnEnable()
        {
            // Subscribe event to GameManager
            GameController.Instance.OnGameStart += InitializedPool;
        }

        private void OnDisable()
        {
            // Unsubscribe event from GameManager
            GameController.Instance.OnGameStart -= InitializedPool;
        }

        #region Private Methods
        /// <summary>
        /// Initilaize the first pool once game starting by GameManager
        /// </summary>
        private void InitializedPool()
        {
            for (int i = 0; i < EnemiesAmount; i++)
            {
                CreatePool(m_EnemyPrefab, m_EnemyList, m_EnemiesPool);
            }

            for (int i = 0; i < m_BulletAmount; i++)
            {
                CreatePool(m_BulletPrefab, m_BulletList, m_BulletPool);
            }

            for (int i = 0; i < 4; i++)
            {
                CreatePool(m_ExplosionEffect, m_BulletList, m_ExplodeEffect);
                CreatePool(laserEffect, m_BulletList, laserEffectPool);
            }
        }

        /// <summary>
        /// Spawn the object and insert it to the object pool, ready to use
        /// </summary>
        /// <param name="objectPool">Object to instantiate in the pool</param>
        /// <param name="poolParent">Parent of all the objects inside the pool</param>
        /// <param name="poolQueue">Object pooling queue variable used</param>
        private void CreatePool(GameObject objectPool, GameObject poolParent, Queue<GameObject> poolQueue)
        {
            GameObject targetPool = Instantiate(objectPool, poolParent.transform);
            targetPool.SetActive(false);
            poolQueue.Enqueue(targetPool);
        }

        /// <summary>
        /// Get the object out from the pool
        /// </summary>
        /// <param name="pool">Object pooling queue variable used</param>
        /// <param name="objectPrefab">Object to instantiate in the pool</param>
        /// <param name="objectParent">Parent of all the objects inside the pool</param>
        /// <returns>Object taken from the pool</returns>
        private GameObject GetObject(Queue<GameObject> pool, GameObject objectPrefab, GameObject objectParent)
        {
            if (pool.Count == 0) CreatePool(objectPrefab, objectParent, pool);

            GameObject objectPool = pool.Dequeue();
            objectPool.SetActive(true);
            return objectPool;
        }

        /// <summary>
        /// Return the object back to the pool
        /// </summary>
        /// <param name="objectPool">Object to return</param>
        /// <param name="pool">Object pooling queue variable used</param>
        private void ReturnObject(GameObject objectPool, Queue<GameObject> pool)
        {
            objectPool.SetActive(false);
            pool.Enqueue(objectPool);
        }
        #endregion

        /// <summary>
        /// Get the enemy object out from the enemy's pool
        /// </summary>
        /// <returns>Enemy object taken from the enemy's pool</returns>
        public GameObject GetEnemy()
        {
            return GetObject(m_EnemiesPool, m_EnemyPrefab, m_EnemyList);
        }

        /// <summary>
        /// Get the bullet object out from the enemy's pool
        /// </summary>
        /// <returns>Bullet object taken from the bullet's pool</returns>
        public GameObject GetBullet()
        {
            return GetObject(m_BulletPool, m_BulletPrefab, m_BulletList);
        }

        /// <summary>
        /// Get the explode effect out from the particle's pool
        /// </summary>
        /// <returns>Explode particle taken from the pool</returns>
        public GameObject GetExplodeEffect()
        {
            GameObject effect = GetObject(m_ExplodeEffect, m_ExplosionEffect, m_BulletList);
            StartCoroutine(ReturnEffect(effect, m_ExplodeEffect));
            return effect;
        }

        public GameObject GetLaserEffect()
        {
            GameObject effect = GetObject(laserEffectPool, laserEffect, m_BulletList);
            StartCoroutine(ReturnEffect(effect, laserEffectPool));
            return effect;
        }

        public GameObject GetRoad()
        {
            return GetObject(m_RoadPool, m_Roads[0], m_RoadList);
        }

        /// <summary>
        /// Return the explode particle to the pool
        /// </summary>
        /// <param name="effect">Particle gameObject to return</param>
        private void ReturnExplode(GameObject effect)
        {
            ReturnObject(effect, m_ExplodeEffect);
        }

        /// <summary>
        /// Return the bullet object to the pool
        /// </summary>
        /// <param name="bullet">Bullet gameObject to return</param>
        public void ReturnBullet(GameObject bullet)
        {
            ReturnObject(bullet, m_BulletPool);
        }

        /// <summary>
        /// Return the enemy object to the pool
        /// </summary>
        /// <param name="enemy">Enemy gameObject to return</param>
        public void ReturnEnemy(GameObject enemy)
        {
            ReturnObject(enemy, m_EnemiesPool);
            GameController.Instance.OnEnemyDestroy();
        }

        public void ReturnRoad(GameObject road)
        {
            ReturnObject(road, m_RoadPool);
            GameController.Instance.OnRoadLoop();
        }

        /// <summary>
        /// Coroutine for particle effect
        /// Start with default timer 0.6 seconds
        /// </summary>
        /// <param name="effect">Explode particle gameObject to return</param>
        /// <returns>Default timer</returns>
        private IEnumerator ReturnEffect(GameObject effect, Queue<GameObject> pool)
        {
            yield return new WaitForSeconds(.6f);
            ReturnObject(effect, pool);
        }
    }
}