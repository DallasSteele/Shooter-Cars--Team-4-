using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Renderer carBodyRenderer; // Reference to the renderer for the car's body

    // This function applies a new material to the car's body
    public void ApplyCarSkin(Material newSkinMaterial)
    {
        carBodyRenderer.material = newSkinMaterial;
    }
}