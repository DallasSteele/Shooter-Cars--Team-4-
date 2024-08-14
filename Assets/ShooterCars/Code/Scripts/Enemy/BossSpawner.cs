using UnityEngine;
using UnityEngine.Playables;

using ShooterCar.Manager;

namespace ShooterCar.Enemy
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_Boss;

        [SerializeField] private PlayableDirector m_Director;

        [SerializeField] private Transform m_BossSpawnPos;

        private GameObject Boss { get; set; }

        private void OnEnable()
        {
            GameController.Instance.OnBossSpawn += SpawnBoss;
        }

        private void OnDisable()
        {
            GameController.Instance.OnBossSpawn -= SpawnBoss;
        }

        private void SpawnBoss()
        {
            if (Boss == null)
            {
                Boss = Instantiate(m_Boss, m_BossSpawnPos);
                m_Director.gameObject.SetActive(true);
                return;
            }
            return;
        }
    }
}