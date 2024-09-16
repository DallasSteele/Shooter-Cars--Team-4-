using UnityEngine;
using UnityEngine.UI;

using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Enemy
{
    public class BossHealth : HealthSystem
    {
        [SerializeField] private EnemyShoot bossShoot;
        [SerializeField] private float duration;

        private Slider healthBar;

        private bool stopChecking;
        private float currentDuration;

        private void Update()
        {
            CountdownBossTimer();
        }

        private void OnEnable()
        {
            GameController.Instance.OnBossDefeated += ResetBoss;
            GameController.Instance.OnGameRestart += ResetBoss;

            InitialStatus();
        }

        private void OnDisable()
        {
            GameController.Instance.OnBossDefeated -= ResetBoss;
            GameController.Instance.OnGameRestart -= ResetBoss;
        }

        private void CountdownBossTimer()
        {
            if (stopChecking) return;

            if (currentDuration <= duration + 7)
            {
                currentDuration += Time.deltaTime;
            }
            else
            {
                GameController.Instance.OnGameRestart();
                stopChecking = true;
            }
        }

        private void ResetBoss()
        {
            bossShoot.SetFireInterval(7);
        }

        protected override void InitialStatus()
        {
            currentDuration = 0;
            stopChecking = false;

            if (m_CurrentHealth <= 0)
                MaxHealth();

            SetHealthBar();
        }

        protected override void Die()
        {
            GameController.Instance.OnBossDefeated();
        }

        protected override void SetHealthBar()
        {
            healthBar = InterfaceHandle.Instance.bossHealthBar;
            healthBar.maxValue = maxHealth;
            healthBar.value = m_CurrentHealth;
        }
    }
}