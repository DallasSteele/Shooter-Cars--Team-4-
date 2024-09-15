using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Player
{
    public class PlayerHealth : HealthSystem
    {
        protected override void Die()
        {
            GameController.Instance.OnGameOver();
        }

        protected override void SetHealthBar()
        {
            throw new System.NotImplementedException();
        }
    }
}
