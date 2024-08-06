using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float m_MaxHP;

        private float m_CurrentHealth;

        private void Awake()
        {
            m_CurrentHealth = m_MaxHP; 
        }

        private void Die()
        {
            GameController.Instance.OnGameOver?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            m_CurrentHealth -= damage;
            if(m_CurrentHealth <= 0) Die();
        }
    }
}
