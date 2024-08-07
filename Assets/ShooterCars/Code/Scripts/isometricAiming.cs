using UnityEngine;

using ShooterCar.Manager;
using ShooterCar.Utilities;

namespace BarthaSzabolcs.IsometricAiming
{
    public class isometricAiming : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform muzzleTransform;
        [SerializeField] private float projectileForce = 20f; // Adjust the force as needed

        #endregion

        #region Private Fields

        [SerializeField] private Vector3 m_Offset;

        private Camera mainCamera;
        private RaycastHit hit;

        #endregion

        #endregion

        #region Methods

        #region Unity Callbacks

        private void Start()
        {
            // Cache the camera, Camera.main is an expensive operation.
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (GameController.Instance.HoverButton) return;

            Aim();

            // Shoot input
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        #endregion

        private void Aim()
        {
            var (success, position) = GetMousePosition();
            if (success)
            {
                // Calculate the direction
                var direction = position - transform.position;

                // Make the transform look in the direction.
                transform.forward = direction;
            }
        }

        private void Shoot()
        {
            // Get the muzzle position and forward direction
            Vector3 muzzlePosition = muzzleTransform.position;
            Vector3 muzzleForward = muzzleTransform.forward;

            // Instantiate the projectile at the muzzle position and rotation
            //GameObject projectile = Instantiate(projectilePrefab, muzzlePosition, Quaternion.LookRotation(muzzleForward));
            GameObject projectile = ObjectPooling.Instance.GetBullet();
            projectile.transform.position = transform.position;
            Projectile bullet = projectile.GetComponent<Projectile>();
            //bullet.IgnoreObject = transform.parent.gameObject;
            //bullet.Offset = m_Offset;
            //bullet.Direction = Vector3.zero + m_Offset - transform.position;
            bullet.Muzzle = transform;
            bullet.IgnoreObject = gameObject.tag;
            projectile.transform.rotation = Quaternion.LookRotation(muzzleForward);

            // Get the Rigidbody component of the projectile
            //Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

            // Set the projectile's velocity to a fixed value
            //projectileRigidbody.velocity = muzzleForward * projectileForce;
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                // The Raycast hit something, return with the position.
                return (success: true, position: hit.point);
            }
            else
            {
                // The Raycast did not hit anything.
                return (success: false, position: Vector3.zero);
            }
        }
        #endregion
    }
}