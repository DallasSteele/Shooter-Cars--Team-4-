using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Utilities;
using ShooterCar.SO;

namespace BarthaSzabolcs.IsometricAiming
{
    public class isometricAiming : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Weapon m_WeaponData;
        [SerializeField] private Transform m_WeaponSlot;
        [SerializeField] private RectTransform crosshair;
        [SerializeField] private float aimSpeed;

        #endregion

        #region Private Fields

        private Camera mainCamera;
        private RaycastHit hit;
        private WeaponView m_Weapon;

        private Vector2 lastTouchPosition;
        private bool isTouching;

        private float m_NextShot;

        #endregion

        #endregion

        #region Methods

        #region Unity Callbacks

        private void Start()
        {
            // Cache the camera, Camera.main is an expensive operation.
            mainCamera = Camera.main;
            //m_Weapon = new WeaponView(m_WeaponData);
        }

        private void Update()
        {
            //if (GameController.Instance.GameStarting == false) return;
            if (GameController.Instance.HoverButton) return;


            if(Input.GetButton("Fire1"))
            {
                Aim();
                Shoot();
            }
        }

        #endregion

        private void Aim()
        {
            var (success, position) = GetMousePosition();
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Capture the initial touch position
                lastTouchPosition = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                // Calculate how much the touch has moved
                Vector2 deltaPosition = touch.position - lastTouchPosition;

                // Move the crosshair by the delta amount
                crosshair.anchoredPosition += deltaPosition * aimSpeed;

                // Update the last touch position for the next frame
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                // Stop tracking touch movement when the touch ends
                isTouching = false;
            }
        }

        private void Shoot()
        {
            // Get the muzzle position and forward direction
            //Vector3 muzzlePosition = muzzleTransform.position;
            //Vector3 muzzleForward = muzzleTransform.forward;

            // Instantiate the projectile at the muzzle position and rotation
            //GameObject projectile = Instantiate(projectilePrefab, muzzlePosition, Quaternion.LookRotation(muzzleForward));
            //projectile.transform.position = muzzlePosition;

            //GameObject projectile = ObjectPooling.Instance.GetBullet();
            //Projectile bullet = projectile.GetComponent<Projectile>();
            //bullet.Shoot(muzzleTransform, hit.point, gameObject.tag);

            //bullet.IgnoreObject = transform.parent.gameObject;
            //bullet.Offset = m_Offset;
            //bullet.Direction = Vector3.zero + m_Offset - transform.position;
            //bullet.Muzzle = transform;
            //projectile.SetActive(true);

            m_Weapon.Shoot(hit.point, gameObject.tag);
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = mainCamera.ScreenPointToRay(crosshair.position);

            if (Physics.Raycast(ray, out hit, 100, groundMask))
            {
                //if (m_WeaponData.Muzzle != null)
                //Debug.DrawLine(m_WeaponData.Muzzle.position, hit.point, Color.red);
                // The Raycast hit something, return with the position.
                return (success: true, position: hit.point);
            }
            else
            {
                // The Raycast did not hit anything.
                //return (success: false, position: Vector3.zero);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
                // The Raycast did not hit anything.
                return (success: false, position: ray.origin + ray.direction * 100);
            }
        }
        #endregion
    }
}