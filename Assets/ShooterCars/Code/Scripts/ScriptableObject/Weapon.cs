using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Utilities;

namespace ShooterCar.SO
{
    [CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObject/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private EnumStore.Bullet m_BulletType;
        [SerializeField] private float m_DamageAmount;
        [SerializeField] private float m_BulletSpeed;
        [SerializeField] private float m_FireRate;
        [SerializeField] private WeaponStore m_Model;
        [SerializeField] private AudioClip m_SFX;

        private Transform m_Muzzle;

        public float DamageAmount { get { return m_DamageAmount; } }
        public float BulletSpeed { get { return m_BulletSpeed; } }
        public float FireRate { get { return m_FireRate; } }
        public AudioClip Sound { get { return m_SFX; } }
        public Transform Muzzle { get { return m_Muzzle; } }

        public GameObject InitializePrefab(Transform weaponSlot)
        {
            WeaponStore gunPrefab = Instantiate(m_Model, weaponSlot);
            m_Muzzle = gunPrefab.Muzzle;
            return gunPrefab.gameObject;
        }
    }

    public class WeaponView
    {
        private Weapon m_Weapon;
        private float m_NextShot;

        public WeaponView(Weapon weapon)
        {
            m_Weapon = weapon;
            m_NextShot = 0;
        }

        public void Initialize(Transform weaponSlot)
        {
            m_Weapon.InitializePrefab(weaponSlot);
        }

        public void Shoot(bool isOn, Vector3 target, string ignoreObject)
        {
            if (Time.time >= m_NextShot)
            {
                if(isOn)
                {
                    Projectile bullet = ObjectPooling.Instance.GetBullet().GetComponent<Projectile>();
                    bullet.Initialize(m_Weapon.DamageAmount, m_Weapon.BulletSpeed);
                    bullet.Shoot(m_Weapon.Muzzle, target, ignoreObject);
                    AudioManager.Instance.PlaySFX(m_Weapon.Sound);
                    m_NextShot = Time.time + 1 / m_Weapon.FireRate;
                }
            }
        }
    }
}