using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] m_CarPos;

        private void SetCarPosition()
        {
            for (int i = 0; i < ObjectPooling.Instance.EnemiesAmount; i++)
            {
                ObjectPooling.Instance.GetEnemy().transform.position = m_CarPos[i].position;
            }
        }

        private void OnEnable()
        {
            GameController.Instance.OnGameStart += SetCarPosition;
        }

        private void OnDisable()
        {
            GameController.Instance.OnGameStart -= SetCarPosition;
        }
    }
}
