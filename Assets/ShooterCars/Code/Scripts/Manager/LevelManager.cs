using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Enemy
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject lvlcompletepanel;
        public Button nextButton;
        public gameManager GameManager;


        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private BossSpawner bossSpawner;

        private bool isWaitingForNextLevel = false;

        public void Start()
        {
            lvlcompletepanel.SetActive(false);
            isWaitingForNextLevel = false;

            //add logics here
            //sub to boss defeated event
            //GameController.Instance.OnBossDefeated += OnBossDefeated;
        }

        /*private void OnDestroy()
        {
            //unsub to boss defeted event
            GameController.Instance.OnBossDefeated -= OnBossDefeated;
        }*/
    
        private void ResumeGame()
        {
            isWaitingForNextLevel = false;

            //logics more here
            //resume enemy spawning
            enemySpawner.ResumeSpawningEnemies(); //added in the EnemySpawner script
        }

        public bool CanSpawnEnemies()
        {
            return !isWaitingForNextLevel;
        }

        public void OnBossDefeated()
        {
            //nextlevelpanel
            // start the enemy spawning
            isWaitingForNextLevel = true;
            lvlcompletepanel.SetActive(true);

            enemySpawner.StopSpawningEnemies(); //added in the EnemySpawner script
        }
        
        public void OnNextButtonClicked()
        {
            // Hide the level complete panel and resume enemy spawning
            lvlcompletepanel.SetActive(false);
            ResumeGame();
        }

        public void Quit()
        {
            GameManager.QuitGame();
        }

    }
}