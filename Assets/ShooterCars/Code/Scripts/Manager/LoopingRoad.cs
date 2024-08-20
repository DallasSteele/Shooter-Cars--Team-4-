using UnityEngine;

namespace ShooterCar.Manager
{
    public class LoopingRoad : MonoBehaviour
    {
        [SerializeField] private Transform LoopingStart;

        private void OnEnable()
        {
            GameController.Instance.OnRoadLoop += GoLooping;
        }

        private void OnDisable()
        {
            GameController.Instance.OnRoadLoop -= GoLooping;
        }

        private void GoLooping()
        {
            GameObject road = ObjectPooling.Instance.GetRoad();
            road.transform.position = LoopingStart.position;
        }
    }
}