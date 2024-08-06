using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Utilities;

namespace ShooterCar.Enemy
{
    public class EnemyShoot : MonoBehaviour
    {
        [SerializeField] private Transform m_Muzzle;
        [SerializeField] private float m_FireCooldown;

        private float m_FireInterval;

        private void Start()
        {
            m_FireInterval = 2;
        }

        private void Update()
        {
            ShootingLoop();
            //MuzzleFacingPlayer();
        }

        private void ShootingLoop()
        {
            if(m_FireInterval <= 0)
            {
                Fire();
                m_FireInterval = m_FireCooldown;
            }
            else
            {
                m_FireInterval -= Time.deltaTime;
            }
        }

        private void MuzzleFacingPlayer()
        {
            m_Muzzle.transform.LookAt(GameController.Instance.Player.transform.position);
        }

        private void Fire()
        {
            GameObject bullet = ObjectPooling.Instance.GetBullet();
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.Shoot(m_Muzzle, GameController.Instance.Player.transform.position, gameObject.tag);
        }
    }
}
