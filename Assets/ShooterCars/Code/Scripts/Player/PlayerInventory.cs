using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    private List<string> ownedItems = new List<string>();
    private void awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //add item to  inventory
    public void AddItem(string itemName)
    {
        if (!ownedItems.Contains(itemName))
        {
            ownedItems.Add(itemName);
            SaveInventory();
        }
    }

    //check if player own the item
    public bool HasItem(string itemName)
    {
        return ownedItems.Contains(itemName);
    }

    //save the player's inventory
    private void SaveInventory()
    {
        //convert list to string to save in playerprefs (simple solution)
        PlayerPrefs.SetString("OwnedItems", string.Join(",", ownedItems));
        PlayerPrefs.Save();
    }

    //load the player's inventory
    private void LoadInventory()
    {
        string savedItems = PlayerPrefs.GetString("OwnedItems", "");
        if (!string.IsNullOrEmpty(savedItems))
        {
            ownedItems = new List<string>(savedItems.Split(','));
        }
    }

    private void Start()
    {
        LoadInventory();
    }

}
