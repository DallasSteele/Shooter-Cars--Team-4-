using System.Collections.Generic;

using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] m_CarPos, m_CarStartPos;
        [SerializeField] private int m_MaxEnemyCount;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GameObject enemySpawner;

        private int m_EnemyCarCount;
        private int x;

        private Dictionary<GameObject, int> m_EnemyPairs { get; set; } = new Dictionary<GameObject, int>();

        private bool isSpawningEnabled { get; set; } = true;

        private void OnEnable()
        {
            GameController.Instance.OnGameStart += SetInitialEnemy;
            GameController.Instance.OnEnemyDestroy += ReproduceEnemy;
            GameController.Instance.OnBossDefeated += ResetEnemy;
            GameController.Instance.OnGameRestart += ResetEnemy;
        }

        private void OnDisable()
        {
            GameController.Instance.OnGameStart -= SetInitialEnemy;
            GameController.Instance.OnEnemyDestroy -= ReproduceEnemy;
            GameController.Instance.OnBossDefeated -= ResetEnemy;
            GameController.Instance.OnGameRestart -= ResetEnemy;
        }

        private void SetInitialEnemy()
        {
            if (levelManager.CanSpawnEnemies()) //check if enemies can spawn
            {
                for (int i = 0; i < ObjectPooling.Instance.EnemiesAmount; i++)
                {
                    GameObject enemy = GetEnemy();
                    m_EnemyPairs.Add(enemy, i);
                    SetEnemyPos(enemy, m_CarStartPos[i].position);
                }       
            }
        }

        private void ReproduceEnemy()
        {
            if (levelManager.CanSpawnEnemies()) //check if enemies can spawn
            {
                if (m_EnemyCarCount >= m_MaxEnemyCount)
                {
                    x++;
                    if(x >= ObjectPooling.Instance.EnemiesAmount)
                    {
                        Debug.LogWarning("Lawan Boss");
                        GameController.Instance.OnBossSpawn();
                    }

                    return;
                }

                SpawnEnemy();
            }
        }

        private void ResetEnemy()
        {
            if (levelManager.CanSpawnEnemies()) //check if enemies can spawn
            {
                m_EnemyCarCount = 0;
                x = 0;

                for (int i = 0; i < ObjectPooling.Instance.EnemiesAmount; i++)
                {
                    GameObject enemy = GetEnemy();
                    SetEnemyPos(enemy, m_CarStartPos[i].position);
                }
            }
        }
        private void SpawnEnemy()
        {
            GameObject enemy = GetEnemy();
            if (m_EnemyPairs.TryGetValue(enemy, out var pair))
            {
                SetEnemyPos(enemy, m_CarStartPos[pair].position);
            }
        }

        private void SetEnemyPos(GameObject enemy, Vector3 startPos)
        {
            enemy.transform.position = startPos;

        }

        private GameObject GetEnemy()
        {
            m_EnemyCarCount++;
            return ObjectPooling.Instance.GetEnemy();
        }

        public void StopSpawningEnemies()
        {
            isSpawningEnabled = false;
        }

        public void ResumeSpawningEnemies()
        {
            if (levelManager.CanSpawnEnemies())
            {
                isSpawningEnabled = true;
                ResetEnemy();
            }
        }
    }
}
