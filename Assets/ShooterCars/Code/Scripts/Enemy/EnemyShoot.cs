using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Parent;

namespace ShooterCar.Enemy
{
    public class EnemyShoot : ShootingSystem
    {
        private Transform m_Player { get { return GameController.Instance.Player.transform; } }

        private float m_FireInterval;

        private void OnEnable()
        {
            m_FireInterval = 2;
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
