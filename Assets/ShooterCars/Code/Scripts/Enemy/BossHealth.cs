using ShooterCar.Manager;
using ShooterCar.Parent;

namespace ShooterCar.Enemy
{
    public class BossHealth : HealthSystem
    {
        protected override void Die()
        {
            GameController.Instance.OnBossDefeated();
        }
    }
}