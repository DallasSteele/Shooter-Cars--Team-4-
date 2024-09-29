using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSkinManager : MonoBehaviour
{
    public Renderer carRenderer; // Reference to the car's renderer component
    public Material defaultSkin; // Default skin
    private Material currentSkin;

    private void Start()
    {
        // Load the previously applied skin if available, otherwise use default skin
        string savedSkin = PlayerPrefs.GetString("SelectedCarSkin", "default");
        ApplySkin(savedSkin == "default" ? defaultSkin : SkinDatabase.Instance.GetSkinMaterial(savedSkin));
    }

    // Apply the selected skin to the car
    public void ApplySkin(Material newSkin)
    {
        currentSkin = newSkin;
        carRenderer.material = currentSkin;
    }

    // Save the selected skin
    public void SaveSkin(string skinName)
    {
        PlayerPrefs.SetString("SelectedCarSkin", skinName);
    }
}