using UnityEngine;

using ShooterCar.Enemy;

namespace ShooterCar.Utilities
{
    public class TankMuzzle : MonoBehaviour 
    {
        [SerializeField] private BossShoot bossShoot;
        [SerializeField] private GameObject muzzleFlash;

        private void Update() 
        {
            if(bossShoot.m_FireInterval <= 0)
                muzzleFlash.SetActive(true);
        }
    }
}