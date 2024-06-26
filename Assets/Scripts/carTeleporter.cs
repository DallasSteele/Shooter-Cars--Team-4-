using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carTeleporter : MonoBehaviour
{
    public Transform startPortal;
    public Transform endPortal;
    public GameObject cameraObject;
    public float cameraDistanceFromCar = 5f;
    public float cameraHeight = 2f;
    public float cameraFieldOfView = 60f; // New field of view variable

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a car
        if (other.CompareTag("Car"))
        {
            TeleportCarAndCamera(other.gameObject);
        }
    }

    private void TeleportCarAndCamera(GameObject car)
    {
        // Get the current position of the car
        Vector3 carPosition = car.transform.position;

        // Calculate the new position for the car
        float deltaZ = carPosition.z - endPortal.position.z;
        Vector3 newCarPosition = new Vector3(
            startPortal.position.x,
            startPortal.position.y,
            startPortal.position.z + deltaZ
        );

        // Teleport the car to the new position
        car.transform.position = newCarPosition;

        // Calculate the new camera position
        Vector3 newCameraPosition = new Vector3(
            newCarPosition.x,
            newCarPosition.y + cameraHeight,
            newCarPosition.z - cameraDistanceFromCar
        );

        // Teleport the camera to the new position
        cameraObject.transform.position = newCameraPosition;
        cameraObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Reset camera rotation

        // Adjust the camera's field of view
        Camera cameraComponent = cameraObject.GetComponent<Camera>();
        cameraComponent.fieldOfView = cameraFieldOfView;
    }
}