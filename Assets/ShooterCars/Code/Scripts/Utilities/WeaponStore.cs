using UnityEngine;

namespace ShooterCar.Utilities
{
    public class WeaponStore : MonoBehaviour
    {
        [SerializeField] private Transform m_Muzzle;

        public Transform Muzzle {  get { return m_Muzzle; } }
    }
}