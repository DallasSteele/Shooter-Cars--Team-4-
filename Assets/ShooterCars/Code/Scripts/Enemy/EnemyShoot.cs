using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Parent;

namespace ShooterCar.Enemy
{
    public class EnemyShoot : ShootingSystem
    {
        private float m_FireInterval;

        private void Start()
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

            m_Weapon.Shoot(GameController.Instance.Player.transform.position, gameObject.tag);
        }
    }
}
