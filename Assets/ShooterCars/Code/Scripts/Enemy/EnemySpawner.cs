using System.Collections.Generic;

using UnityEngine;

using ShooterCar.Manager;

using DG.Tweening;

namespace ShooterCar.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] m_CarPos, m_CarStartPos;
        [SerializeField] private int m_MaxEnemyCount;

        private int m_EnemyCarCount;

        private Dictionary<GameObject, int> m_EnemyPairs = new Dictionary<GameObject, int>();

        private void OnEnable()
        {
            GameController.Instance.OnGameStart += SetInitialEnemy;
            GameController.Instance.OnEnemyDestroy += SetEnemyPosition;
        }

        private void OnDisable()
        {
            GameController.Instance.OnGameStart -= SetInitialEnemy;
            GameController.Instance.OnEnemyDestroy -= SetEnemyPosition;
        }

        private void SetInitialEnemy()
        {
            for (int i = 0; i < ObjectPooling.Instance.EnemiesAmount; i++)
            {
                GameObject enemy = GetEnemy();
                m_EnemyPairs.Add(enemy, i);
                enemy.transform.position = m_CarStartPos[i].position;
                enemy.transform.DOMove(m_CarPos[i].position, 1);
            }
        }

        private void SetEnemyPosition()
        {
            if (m_EnemyCarCount >= m_MaxEnemyCount - ObjectPooling.Instance.EnemiesAmount) return;

            GameObject enemy = GetEnemy();
            if (m_EnemyPairs.TryGetValue(enemy, out var pair))
            {
                enemy.transform.position = m_CarStartPos[pair].position;
                enemy.transform.DOMove(m_CarPos[pair].position, 2);
            }
        }

        private GameObject GetEnemy()
        {
            m_EnemyCarCount++;
            return ObjectPooling.Instance.GetEnemy();
        }
    }
}
