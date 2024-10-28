using UnityEngine;

namespace ShooterCar.SO
{
    [CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObject/Weapon/Laser", order = 0)]
    public class WeaponLaser : WeaponView
    {
        public WeaponLaser(Weapon weapon, Transform muzzle) : base(weapon, muzzle)
        {
        }
    }
}