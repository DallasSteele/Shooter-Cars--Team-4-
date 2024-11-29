using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Utilities;

namespace ShooterCar.SO
{
    [CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObject/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private float m_DamageAmount;
        [SerializeField] private float m_DamageSpread;
        [SerializeField] private float m_BulletSpeed;
        [SerializeField] private float m_FireRate;

        [SerializeField] private EnumStore.Bullet m_BulletType;
        
        [SerializeField] private WeaponStore m_Model;
        
        [SerializeField] private AudioClip m_SFX;

        [SerializeField] private TrailRenderer trail;
        
        public float DamageAmount { get { return m_DamageAmount; } }
        public float DamageSpread { get { return m_DamageSpread; } }
        public float BulletSpeed { get { return m_BulletSpeed; } }
        public float FireRate { get { return m_FireRate; } }

        public EnumStore.Bullet BulletType { get { return m_BulletType; } }
        
        public AudioClip Sound { get { return m_SFX; } }

        public TrailRenderer Trail { get { return trail; } }

        public WeaponStore InitializePrefab(Transform weaponSlot)
        {
            WeaponStore gunPrefab = Instantiate(m_Model, weaponSlot);
            return gunPrefab;
        }
    }

    public class WeaponView
    {
        private Weapon m_Weapon;

        private Transform m_Muzzle;
        
        private float m_NextShot;

        public WeaponView(Weapon weapon, Transform muzzle)
        {
            m_Weapon = weapon;
            m_Muzzle = muzzle;
            m_NextShot = 0;
        }

        public void Shoot(Vector3 target, string ignoreObject, LineRenderer laser = null)
        {
            m_Muzzle.transform.LookAt(target);
            
            if (Time.time >= m_NextShot)
            {
                Projectile bullet = ObjectPooling.Instance.GetBullet().GetComponent<Projectile>();

                if (m_Weapon.BulletType == EnumStore.Bullet.Laser && laser != null)
                {
                    laser.enabled = true;
                    laser.SetPosition(0, m_Muzzle.position);
                    laser.SetPosition(1, target);
                    ObjectPooling.Instance.GetLaserEffect().transform.position = target;
                }

                bullet.Initialize(m_Weapon.BulletType, m_Weapon.DamageAmount, m_Weapon.DamageSpread, m_Weapon.BulletSpeed, m_Weapon.Trail);
                bullet.Shoot(m_Muzzle, target, ignoreObject);

                AudioManager.Instance.PlaySFX(m_Weapon.Sound);

                m_NextShot = Time.time + 1 / m_Weapon.FireRate;
            }
        }
    }
}