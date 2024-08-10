using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Parent;

namespace ShooterCar.Player
{
    public class PlayerShoot : ShootingSystem
    {
        [SerializeField] private LayerMask m_AvailableLayer;

        private Camera m_Camera { get { return GameController.Instance.MainCamera; } }

        private RaycastHit hit;

        protected override void Update()
        {
            if (GameController.Instance.HoverButton) return;

            if(Input.GetButton("Fire1"))
            {
                GetMousePosition();
                Shoot();
            }
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = m_Camera.ScreenPointToRay(Input.mousePosition);

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
            m_Weapon.Shoot(hit.point, gameObject.tag);
        }
    }
}