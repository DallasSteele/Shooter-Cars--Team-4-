using UnityEngine;

public class folcam : MonoBehaviour
{
    public Transform target; // The car GameObject that the camera will follow
    public float distance = 5f; // The distance between the camera and the target
    public float height = 2f; // The height of the camera above the target
    public float smoothTime = 0.25f; // The time it takes for the camera to smoothly transition
    public float cameraRotationX = 0f; // The rotation of the camera on the X-axis

    private Vector3 velocity = Vector3.zero;
    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the desired camera position and rotation
        CalculateCameraTransform();

        // Smoothly move and rotate the camera towards the desired transform
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothTime);
    }

    private void CalculateCameraTransform()
    {
        // Calculate the desired camera position
        desiredPosition = new Vector3(0f, target.position.y + height, target.position.z - distance); // Fix the X-position to 0

        // Calculate the desired camera rotation
        desiredRotation = Quaternion.Euler(cameraRotationX, 0f, 0f) * Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }
}