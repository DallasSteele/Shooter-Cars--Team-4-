using UnityEngine;
using TMPro;
using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Player
{
    public class PlayerHealth : HealthSystem
    {
        [SerializeField] private Transform healthBar;
        [SerializeField] private GameObject gameOverUI; //UI gameover added
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

                //check if health is zero
                if (m_CurrentHealth <= 0)
                {
                    Die();
                }
            }
        }

        public override void TakeDamage(float damageAmount)
        {
            base.TakeDamage(damageAmount);

            GameController.Instance.OnHit();
        }

        protected override void Die()
        {
            //show gameover UI
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);
            GameController.Instance.OnGameOver();
        }

        protected override void SetHealthBar()
        {
            healthBar.localScale = new Vector2(m_CurrentHealth / maxHealth, healthBar.localScale.y);
        }
    }
}
