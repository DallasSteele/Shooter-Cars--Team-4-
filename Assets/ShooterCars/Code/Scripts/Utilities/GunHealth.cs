using ShooterCar.BaseClass;

namespace ShooterCar.Utilities
{
    public class GunHealth : HealthSystem
    {
        protected override void Die()
        {
            Destroy(gameObject);
        }

        protected override void SetHealthBar()
        {
        }
    }
}