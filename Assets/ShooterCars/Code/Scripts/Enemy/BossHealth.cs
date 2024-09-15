using UnityEngine;
using UnityEngine.UI;

using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Enemy
{
    public class BossHealth : HealthSystem
    {
        [SerializeField] private float duration;

        private Slider healthBar;
        private float currentDuration;

        private void Update()
        {
            if (currentDuration <= duration)
                currentDuration += Time.fixedDeltaTime;
            else
                GameController.Instance.OnGameRestart();
        }

        private void OnDisable()
        {
            healthBar.transform.parent.gameObject.SetActive(false);
        }

        protected override void InitialStatus()
        {
            if (m_CurrentHealth <= 0)
                MaxHealth();

            SetHealthBar();
        }

        protected override void Die()
        {
            healthBar.transform.parent.gameObject.SetActive(false);

            GameController.Instance.OnBossDefeated();
        }

        protected override void SetHealthBar()
        {
            currentDuration = 0;

            healthBar = InterfaceHandle.Instance.bossHealthBar;
            healthBar.transform.parent.gameObject.SetActive(true);
            healthBar.maxValue = maxHealth;
            healthBar.value = m_CurrentHealth;
        }
    }
}