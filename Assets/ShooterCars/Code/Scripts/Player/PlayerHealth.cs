using ShooterCar.Manager;
using ShooterCar.Parent;

namespace ShooterCar.Player
{
    public class PlayerHealth : HealthSystem
    {
        protected override void Die()
        {
            GameController.Instance.OnGameOver?.Invoke();
        }
    }
}
