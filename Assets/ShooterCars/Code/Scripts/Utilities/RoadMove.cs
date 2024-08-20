using UnityEngine;

namespace ShooterCar.Utilities
{
    public class RoadMove : MonoBehaviour
    {
        private float speed = 8;

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime * -transform.forward);
        }
    }
}