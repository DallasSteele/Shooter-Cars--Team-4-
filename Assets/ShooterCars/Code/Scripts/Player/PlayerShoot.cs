using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.BaseClass;

namespace ShooterCar.Player
{
    public class PlayerShoot : ShootingSystem
    {
        [SerializeField] private LayerMask m_AvailableLayer;

        [SerializeField] private RectTransform crosshair;
        [SerializeField] private float aimSensitivity;
        
        private Camera m_Camera { get { return GameController.Instance.MainCamera; } }

        private RaycastHit hit;

        private Vector2 lastTouchPosition;
        private bool isTouching;

        private (bool success, Vector3 position) GetTouchPosition()
        {
            Touch touch = Input.GetTouch(0);

            if (Input.GetMouseButtonDown(0))
            {
                lastTouchPosition = Input.mousePosition;
                isTouching = true;
            }
            else if (Input.GetMouseButton(0) && isTouching)
            {
                Vector2 exchangePos = (Vector2)Input.mousePosition - lastTouchPosition;
                crosshair.anchoredPosition += exchangePos * aimSensitivity;
                lastTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isTouching = false;
            }

            var ray = m_Camera.ScreenPointToRay(crosshair.position);

            if (Physics.Raycast(ray, out hit, 100, m_AvailableLayer))
            {
                return (success: true, position: hit.point);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
                return (success: false, position: ray.origin + ray.direction * 100);
            }
        }

        protected override void Shoot()
        {
            if (GameController.Instance.HoverButton) return;

            if (Input.GetButton("Fire1"))
            {
                GetTouchPosition();
                m_Weapon.Shoot(hit.point, gameObject.tag);
            }
            
            if(Input.GetButtonUp("Fire1"))
                GameController.Instance.Line.enabled = false;
        }
    }
}