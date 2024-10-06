using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Enemy
{
    public class BossShoot : ShootingSystem
    {
        [SerializeField] private float m_Cooldown = 2;

        private float m_FireInterval;

        private Transform m_Player { get { return GameController.Instance.Player.transform; } }

        private void OnEnable()
        {
            m_FireInterval = m_Cooldown;
            weaponModel.AddCollider();
        }

        private bool IsWeaponDestroyed()
        {
            if (weaponModel == null)
                return true;

            return false;
        }

        protected override void Shoot()
        {
            if (IsWeaponDestroyed())
                return;

            if (m_FireInterval >= 0)
            {
                m_FireInterval -= Time.deltaTime;
                return;
            }

            m_Weapon.Shoot(m_Player.position, gameObject.tag);
        }

        public void SetFireInterval(float fireInterval)
        {
            m_FireInterval = fireInterval;
        }
    }
}
