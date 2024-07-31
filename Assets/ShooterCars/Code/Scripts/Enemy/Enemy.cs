using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Utilities;

namespace ShooterCar.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform m_Muzzle;
        [SerializeField] private float m_FireCooldown;

        private float m_FireInterval;

        private void Update()
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

        private void Fire()
        {
            GameObject bullet = ObjectPooling.Instance.GetBullet();
            bullet.transform.position = m_Muzzle.position;
            bullet.transform.LookAt(GameController.Instance.Player.transform);
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.Muzzle = m_Muzzle;
            projectile.Direction = -bullet.transform.TransformDirection(transform.forward) * 2;
            
            //projectile.IgnoreObject =  //Objek yang memiliki komponen Enemy.cs
        }

        private void OnCollisionEnter(Collision collision)
        {
            ObjectPooling.Instance.ReturnEnemy(gameObject);
        }
    }
}
