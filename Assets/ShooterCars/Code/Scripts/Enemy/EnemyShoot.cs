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
            bullet.transform.position = m_Muzzle.position;
            bullet.transform.LookAt(GameController.Instance.Player.transform);
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.Muzzle = m_Muzzle;
            projectile.IgnoreObject = gameObject.tag;            
        }
    }
}
