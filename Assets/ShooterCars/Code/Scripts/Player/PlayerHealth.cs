using UnityEngine;

namespace ShooterCar.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float m_MaxHP;
        [SerializeField] private float m_CurrentHealth;

        private void Start()
        {
            m_CurrentHealth = m_MaxHP;
        }

        public void TakeDamage(float damage)
        {
            m_CurrentHealth -= damage;
        }
    }
}
