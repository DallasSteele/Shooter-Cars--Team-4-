using UnityEngine;

namespace ShooterCar.Parent
{
    public abstract class HealthSystem : MonoBehaviour
    {
        [SerializeField] private float m_MaxHealth;

        private float m_CurrentHealth;

        private void OnEnable()
        {
            m_CurrentHealth = m_MaxHealth;
        }

        public void TakeDamage(float damageAmount)
        {
            m_CurrentHealth -= damageAmount;

            if(m_CurrentHealth <= 0)
            {
                Die();
            }
        }

        protected abstract void Die();
    }
}