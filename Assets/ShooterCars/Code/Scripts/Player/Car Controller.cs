using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Renderer carRenderer; // The renderer of the car object
    [SerializeField] private Texture[] carSkins;   // Array of car skins (albedo textures)

    
    public void SetCarSkin(string itemName)
    {
        // Find the corresponding albedo texture based on the skin name
        Texture newSkin = GetCarSkinTexture(itemName);

        if (newSkin != null && carRenderer != null)
        {
            Material carMaterial = carRenderer.material; // Get the car's current material
            carMaterial.SetTexture("_MainTex", newSkin); // Update the albedo texture
            Debug.Log($"Albedo texture for {itemName} applied.");
        }
        else
        {
            Debug.LogError("CarRenderer or skin texture is missing!");
        }
    }

    // This method would return the correct skin texture based on item name
    private Texture GetCarSkinTexture(string itemName)
    {
        // Here, you'd match the item name with the appropriate texture
        // For simplicity, assume itemName is an index or unique name tied to the texture
        for (int i = 0; i < carSkins.Length; i++)
        {
            if (carSkins[i].name == itemName)
            {
                return carSkins[i];
            }
        }

        Debug.LogError("No matching skin texture found!");
        return null;
    }
}









//[SerializeField] private Renderer carBodyRenderer; // Reference to the renderer for the car's body

// This function applies a new material to the car's body
//public void ApplyCarSkin(Color newAlbedoColor)
// {
//    carBodyRenderer.material.SetColor("_Color", newAlbedoColor); //_Color is default shader property for albedo
//}
