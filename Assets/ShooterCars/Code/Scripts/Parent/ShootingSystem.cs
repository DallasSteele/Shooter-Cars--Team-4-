using UnityEngine;

using ShooterCar.SO;
using ShooterCar.Utilities;

namespace ShooterCar.Parent
{
    public abstract class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private Weapon m_WeaponData;

        [SerializeField] private Transform m_WeaponSlot;

        protected WeaponView m_Weapon { get; private set; }

        private void Awake()
        {
            InitializeWeaponView();
        }

        protected virtual void Update()
        {
            Shoot();
        }

        protected abstract void Shoot();

        private void InitializeWeaponView()
        {
            WeaponStore weaponModel = m_WeaponData.InitializePrefab(m_WeaponSlot);
            m_Weapon = new WeaponView(m_WeaponData, weaponModel.Muzzle);
        }
    }
}
