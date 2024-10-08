using UnityEngine;

using ShooterCar.SO;
using ShooterCar.Utilities;

namespace ShooterCar.BaseClass
{
    public abstract class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private Weapon m_WeaponData;

        [SerializeField] private Transform m_WeaponSlot;

        protected WeaponView m_Weapon { get; private set; }
        protected WeaponStore weaponModel { get; private set; }

        public Weapon WeaponData
        {
            get { return m_WeaponData; }
            private set
            {
                if(m_WeaponData == value) return;

                m_WeaponData = value;
                InitializeWeaponView();
            }
        }

        private void Awake()
        {
            InitializeWeaponView();
        }

        private void Update()
        {
            Shoot();
        }

        protected abstract void Shoot();

        private void InitializeWeaponView()
        {
            if(m_WeaponSlot.childCount > 0)
            {
                Destroy(m_WeaponSlot.GetChild(0).gameObject);
            }

            weaponModel = WeaponData.InitializePrefab(m_WeaponSlot);
            m_Weapon = new WeaponView(WeaponData, weaponModel.Muzzle);
        }
    }
}
