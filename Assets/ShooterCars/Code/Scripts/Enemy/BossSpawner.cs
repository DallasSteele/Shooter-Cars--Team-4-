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
        

        private GameObject Boss { get; set; }

        private void OnEnable()
        {
            GameController.Instance.OnBossSpawn += SpawnBoss;
            GameController.Instance.OnBossDefeated += Defeated;
        }

        private void OnDisable()
        {
            GameController.Instance.OnBossSpawn -= SpawnBoss;
            GameController.Instance.OnBossDefeated -= Defeated;
        }

        private void SpawnBoss()
        {
            if (Boss == null)
            {
                Boss = Instantiate(m_Boss, m_BossSpawnPos);

                m_Director.Play(bossSpawn);
                return;
            }
            return;
        }

        private void Defeated()
        {
            Destroy(Boss);

            m_Director.Play(bossDefeat);
        }
    }
}