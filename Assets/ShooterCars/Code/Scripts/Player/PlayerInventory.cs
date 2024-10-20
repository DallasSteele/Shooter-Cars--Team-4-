using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    private List<string> ownedItems = new List<string>();
    private string equippedItem;
    public CarController carController;

    private void Awake()
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

    // Equip an item
    public void EquipItem(string itemName)
    {
        if (HasItem(itemName))
        {
            equippedItem = itemName;
            SaveInventory();
            carController.SetCarSkin(itemName);
            Debug.Log($"Car skin {itemName} applied.");
        }
    }

    // Check if the item is equipped
    public bool IsEquipped(string itemName)
    {
        return equippedItem == itemName;
        // Call the method that updates the car with the new skin  
    }

    //save the player's inventory
    private void SaveInventory()
    {
        //convert list to string to save in playerprefs (simple solution)
        PlayerPrefs.SetString("OwnedItems", string.Join(",", ownedItems));
        PlayerPrefs.SetString("EquippedItem", equippedItem);
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

        equippedItem = PlayerPrefs.GetString("EquippedItem", "");
    }

    private void Start()
    {
        LoadInventory();
    }

}
