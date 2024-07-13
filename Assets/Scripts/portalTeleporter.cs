using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class portalTeleporter : MonoBehaviour
{
    public Transform startPortal;
    public Transform endPortal;

    private Vector3 newCarPosition;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a car
        if (other.CompareTag("Car"))
        {
            TeleportCarAndGun(other.gameObject);
        }
    }

    private void TeleportCarAndGun(GameObject carObject)
    {
        // Get the current position of the car
        Vector3 carPosition = carObject.transform.position;

        // Calculate the new position for the car
        float deltaZ = carPosition.z - endPortal.position.z;
        float deltaX = carPosition.x - endPortal.position.x;
        newCarPosition = new Vector3(
            startPortal.position.x + deltaX,
            startPortal.position.y,
            startPortal.position.z + deltaZ
        );

        // Teleport the car to the new position
        carObject.transform.position = newCarPosition;
    }

}