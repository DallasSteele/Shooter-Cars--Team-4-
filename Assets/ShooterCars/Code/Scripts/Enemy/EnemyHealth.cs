using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Enemy
{
    public class EnemyHealth : HealthSystem
    {
        [SerializeField] private Transform healthBar;

        protected override void Die()
        {
            ObjectPooling.Instance.ReturnEnemy(gameObject);
        }

        protected override void SetHealthBar()
        {
            healthBar.localScale = new Vector2(m_CurrentHealth / maxHealth, healthBar.localScale.y);
        }
    }
}