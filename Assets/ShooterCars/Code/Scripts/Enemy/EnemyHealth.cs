using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float m_MaxHealth;

        private float m_CurrentHealth;

        private void Awake()
        {
            m_CurrentHealth = m_MaxHealth;
        }

        private void Die()
        {
            ObjectPooling.Instance.ReturnEnemy(gameObject);
        }

        public void TakeDamage(float damage)
        {
            m_CurrentHealth -= damage;
            if (m_CurrentHealth <= 0) Die();
        }
    }
}