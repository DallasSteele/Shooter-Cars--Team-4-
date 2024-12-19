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
        public LevelConfigure level {get; private set;}

        [SerializeField] private LevelConfigure[] levels;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private BossSpawner bossSpawner;

        private bool isWaitingForNextLevel = false;
        private int currentLevel;

        public void Start()
        {
            lvlcompletepanel?.SetActive(false);
            isWaitingForNextLevel = false;
            level = levels[currentLevel];

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
            StartCoroutine(ShowCompletePanel());
            enemySpawner.StopSpawningEnemies(); //added in the EnemySpawner script
        }

        private IEnumerator ShowCompletePanel()
        {
            yield return new WaitForSeconds(level.completeDelay);

            lvlcompletepanel?.SetActive(true);
        }
        
        public void OnNextButtonClicked()
        {
            // Hide the level complete panel and resume enemy spawning
            lvlcompletepanel?.SetActive(false);
            currentLevel += 1;
            if(currentLevel > levels.Length) currentLevel = 0;
            level = levels[currentLevel];
            GameController.Instance.OnGameRestart();
            ResumeGame();
        }

        public void Quit()
        {
            GameManager.QuitGame();
        }

    }
}