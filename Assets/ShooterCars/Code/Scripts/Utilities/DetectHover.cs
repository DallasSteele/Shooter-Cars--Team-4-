using UnityEngine;
using UnityEngine.EventSystems;

using ShooterCar.Manager;

namespace ShooterCar.Utilities
{
    public class DetectHover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            GameController.Instance.HoverButton = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            GameController.Instance.HoverButton = false;
        }
    }
}
