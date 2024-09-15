using UnityEngine;
using UnityEngine.Playables;

using ShooterCar.Manager;

namespace ShooterCar.Enemy
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_Boss;

        [SerializeField] private PlayableAsset bossSpawn, bossDefeat;

        [SerializeField] private PlayableDirector m_Director;

        [SerializeField] private Transform m_BossSpawnPos;

        [SerializeField] private LevelManager levelManager;

        [SerializeField] private float bossDuration;

        private GameObject Boss { get; set; }

        private void OnEnable()
        {
            GameController.Instance.OnBossSpawn += SpawnBoss;
            GameController.Instance.OnBossDefeated += Defeated;
            GameController.Instance.OnGameRestart += DisableCurrentBoss;
        }

        private void OnDisable()
        {
            GameController.Instance.OnBossSpawn -= SpawnBoss;
            GameController.Instance.OnBossDefeated -= Defeated;
            GameController.Instance.OnGameRestart -= DisableCurrentBoss;
        }

        private void TrySpawnBoss()
        {
            if (levelManager.CanSpawnEnemies()) // check if boss is allowed to spawn
            {
                SpawnBoss();
            }
        }

        private void SpawnBoss()
        {
            if (Boss == null)
            {
                Boss = Instantiate(m_Boss, m_BossSpawnPos);
            }

            Boss.SetActive(true);
            m_Director.Play(bossSpawn);
        }

        private void Defeated()
        {
            Destroy(Boss);

            m_Director.Play(bossDefeat);
            //notify level manager that the boss is defeated
            levelManager.OnBossDefeated();
        }

        private void DisableCurrentBoss()
        {
            Boss.SetActive(false);
            m_Director.Play(bossDefeat);
        }
    }
}