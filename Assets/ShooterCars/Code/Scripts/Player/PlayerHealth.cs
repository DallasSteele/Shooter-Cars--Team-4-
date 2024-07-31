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

        void IDamageable.TakeDamage(float damage)
        {
            m_CurrentHealth -= damage;
        }
    }
}
