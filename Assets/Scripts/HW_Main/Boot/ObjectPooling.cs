using System.Collections.Generic;

using UnityEngine;

namespace HW.Boot
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
        /// All object pooling queue variables
        /// </summary>
        private Queue<GameObject> m_EnemiesPool = new Queue<GameObject>();
        private Queue<GameObject> m_BulletPool = new Queue<GameObject>();

        /// <summary>
        /// Single Instance for easy call this class from entire project
        /// </summary>
        public static ObjectPooling Instance { get; private set; }

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// Initilaize the first pool once game starting by GameManager
        /// </summary>
        private void InitializedPool()
        {
            for (int i = 0; i < m_EnemiesAmount; i++)
            {
                CreatePool(m_EnemyPrefab, m_EnemyList, m_EnemiesPool);
            }

            for (int i = 0; i < m_BulletAmount; i++)
            {
                CreatePool(m_BulletPrefab, m_BulletList, m_BulletPool);
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
        /// <returns>Object taken from the pool</returns>
        public GameObject GetObject(Queue<GameObject> pool)
        {
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

        private void OnEnable()
        {
            // Subscribe event to GameManager
            GameManager.Instance.OnGameStart += InitializedPool;
        }

        private void OnDisable()
        {
            // Unsubscribe event from GameManager
            GameManager.Instance.OnGameStart -= InitializedPool;
        }
    }
}