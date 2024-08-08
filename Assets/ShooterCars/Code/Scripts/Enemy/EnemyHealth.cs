using ShooterCar.Manager;
using ShooterCar.Parent;

namespace ShooterCar.Enemy
{
    public class EnemyHealth : HealthSystem
    {
        protected override void Die()
        {
            ObjectPooling.Instance.ReturnEnemy(gameObject);
        }
    }
}