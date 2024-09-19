using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Player
{
    public class PlayerHealth : HealthSystem
    {
        [SerializeField] private Transform healthBar;

        [SerializeField] private float healthTimer;

        private float lastHealth;
        private float healthInterval;

        private void Update()
        {
            if(healthInterval < healthTimer)
            {
                healthInterval += Time.deltaTime;
            }
            else
            {
                healthBar.parent.gameObject.SetActive(false);
            }

            if (lastHealth != m_CurrentHealth)
            {
                healthBar.parent.gameObject.SetActive(true);
                healthInterval = 0;
                lastHealth = m_CurrentHealth;
            }
        }

        protected override void Die()
        {
            GameController.Instance.OnGameOver();
        }

        protected override void SetHealthBar()
        {
            healthBar.localScale = new Vector2(m_CurrentHealth / maxHealth, healthBar.localScale.y);
        }
    }
}
