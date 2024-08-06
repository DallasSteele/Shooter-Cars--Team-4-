using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Utilities;

namespace ShooterCar.SO
{
    [CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObject/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private float m_DamageAmount;
        [SerializeField] private float m_BulletSpeed;
        [SerializeField] private float m_FireRate;
        [SerializeField] private GameObject m_Model;
        [SerializeField] private Transform m_Muzzle;
        [SerializeField] private AudioClip m_SFX;
        [SerializeField] private Projectile m_Bullet;

        public float FireRate { get { return m_FireRate; } }

        public Projectile GetProjectile()
        {
            m_Bullet.Initialize(m_DamageAmount, m_BulletSpeed);
            return m_Bullet;
        }
    }

    public class WeaponRuntime
    {
        private Weapon m_Weapon;
        private float m_NextShot;

        public WeaponRuntime(Weapon weapon)
        {
            m_Weapon = weapon;
            m_NextShot = 0;
        }

        public void Shoot()
        {
            if(Time.time >= m_NextShot)
            {
                if(Input.GetButton("Fire1"))
                {
                    GameController.Instance.OnFire?.Invoke();
                    m_NextShot = Time.time + 1 / m_Weapon.FireRate;
                }
            }
        }
    }
}