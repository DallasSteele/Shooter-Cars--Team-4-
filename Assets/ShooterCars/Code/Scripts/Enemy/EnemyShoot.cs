using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Enemy
{
    public class EnemyShoot : ShootingSystem
    {
        [SerializeField] private float m_Cooldown = 2;
        
        private float m_FireInterval;
        
        private Transform m_Player { get { return GameController.Instance.Player.transform; } }

        private void OnEnable()
        {
            m_FireInterval = m_Cooldown;
        }

        protected override void Shoot()
        {
            if (m_FireInterval >= 0)
            {
                m_FireInterval -= Time.deltaTime;
                return;
            }

            m_Weapon.Shoot(m_Player.position, gameObject.tag);
        }
    }
}
