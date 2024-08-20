using UnityEngine;

using ShooterCar.Manager;

namespace ShooterCar.Utilities
{
    public class RoadLimit : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.parent.CompareTag("Road"))
            {
                ObjectPooling.Instance.ReturnRoad(other.transform.parent.gameObject);
            }
        }
    }
}