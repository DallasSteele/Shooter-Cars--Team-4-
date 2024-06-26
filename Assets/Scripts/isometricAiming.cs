using UnityEngine;

namespace BarthaSzabolcs.IsometricAiming
{
    public class isometricAiming : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform muzzleTransform;
        [SerializeField] private float projectileForce = 1000f;

        #endregion
        #region Private Fields

        private Camera mainCamera;

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
            Aim();

            //shoot input
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

                // You might want to delete this line.
                // Ignore the height difference.
                //direction.y = 0;
                //actually, we need the height difference
                //nevermind
                //wait, actually...

                // Make the transform look in the direction.
                transform.forward = direction;
            }
        }

        private void Shoot()
        {
            // Store the current forward direction
            Vector3 currentForward = muzzleTransform.forward;

            // Instantiate a projectile
            GameObject projectile = Instantiate(projectilePrefab, muzzleTransform.position, Quaternion.identity);

            // Apply force to the projectile
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForce, ForceMode.Impulse);
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            {
                // The Raycast hit something, return with the position.
                return (success: true, position: hitInfo.point);
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