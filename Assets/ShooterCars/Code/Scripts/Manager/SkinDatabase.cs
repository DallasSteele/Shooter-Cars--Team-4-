using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SkinDatabase : MonoBehaviour
{
    public static SkinDatabase Instance { get; private set; }

    public List<SkinEntry> skinList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Retrieve the material by skin name
    public Material GetSkinMaterial(string skinName)
    {
        foreach (var skin in skinList)
        {
            if (skin.skinName == skinName)
                return skin.skinMaterial;
        }

        return null;
    }
}

[System.Serializable]
public class SkinEntry
{
    public string skinName;
    public Material skinMaterial;
}