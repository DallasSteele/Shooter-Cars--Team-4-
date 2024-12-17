using UnityEngine;
using UnityEngine.Playables;

using ShooterCar.Manager;
using System.Collections;

namespace ShooterCar.Enemy
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject m_Boss;

        [SerializeField] private PlayableAsset bossSpawn, bossDefeat;

        [SerializeField] private PlayableDirector m_Director;

        [SerializeField] private Transform m_BossSpawnPos;

        [SerializeField] private LevelManager levelManager;

        private GameObject healthBar;
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
                Boss = Instantiate(levelManager.level.boss, m_BossSpawnPos);
            }

            Boss.SetActive(true);
            // m_Director.Play(bossSpawn);

            healthBar = InterfaceHandle.Instance.bossHealthBar.gameObject;
            healthBar.transform.parent.gameObject.SetActive(true);
        }

        private void Defeated()
        {
            DisableCurrentBoss();
            StartCoroutine(WaitForCinematic());
            //notify level manager that the boss is defeated
            levelManager.OnBossDefeated();
        }

        private void DisableCurrentBoss()
        {
            // m_Director.Play(bossDefeat);
            healthBar.transform.parent.gameObject.SetActive(false);
        }

        private IEnumerator WaitForCinematic()
        {
            yield return new WaitForSeconds(6);
            Destroy(Boss);
        }
    }
}