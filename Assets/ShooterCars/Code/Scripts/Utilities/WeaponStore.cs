using UnityEngine;

using ShooterCar.BaseClass;

namespace ShooterCar.Utilities
{
    public class WeaponStore : MonoBehaviour
    {
        [SerializeField] private Transform m_Muzzle;

        public BoxCollider weaponCollider { get; private set; }

        public Transform Muzzle {  get { return m_Muzzle; } }

        private void Awake()
        {
            weaponCollider = GetComponent<BoxCollider>();
            ShootingSystem shoot = GetComponentInParent<ShootingSystem>();
            if (shoot != null)
                tag = shoot.tag;
        }

        public void AddCollider()
        {
            weaponCollider.enabled = true;
        }
    }
}