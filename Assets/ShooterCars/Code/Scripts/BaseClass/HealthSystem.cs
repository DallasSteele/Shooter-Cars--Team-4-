using UnityEngine;

namespace ShooterCar.BaseClass
{
    public abstract class HealthSystem : MonoBehaviour
    {
        [SerializeField] private float m_MaxHealth;

        protected float maxHealth { get { return m_MaxHealth; } }
        protected float m_CurrentHealth { get; private set; }

        private void OnEnable()
        {
            InitialStatus();
        }

        protected virtual void InitialStatus()
        {
            m_CurrentHealth = maxHealth;

            SetHealthBar();
        }

        protected void MaxHealth()
        {
            m_CurrentHealth = maxHealth;
        }

        public virtual void TakeDamage(float damageAmount)
        {
            m_CurrentHealth -= damageAmount;
            if(m_CurrentHealth <= 0)
            {
                Die();
                return;
            }

            SetHealthBar();
        }

        protected abstract void SetHealthBar();
        protected abstract void Die();
    }
}