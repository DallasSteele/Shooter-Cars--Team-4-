using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Enemy
{
    public class JeepHealth : HealthSystem
    {
        protected override void Die()
        {
            ObjectPooling.Instance.ReturnEnemy(gameObject);
        }

        protected override void SetHealthBar()
        {
            throw new System.NotImplementedException();
        }
    }
}