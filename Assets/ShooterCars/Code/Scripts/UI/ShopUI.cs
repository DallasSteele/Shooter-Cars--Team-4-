using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance {  get; private set; }

    [SerializeField] private Transform shopItemContainer;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private Transform carItemContainer;
    [SerializeField] private Transform carSkinItemContainer;
    [SerializeField] private Transform turretSkinItemContainer;
    [SerializeField] private CarController playerCarController;  //ref to the player's car controller

    public ShopItem[] itemsForSale;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeShop();
    }

    //initialize the shop by listing all items
    private void InitializeShop()
    {
        foreach (ShopItem item in itemsForSale)
        {
            if (item == null)
        {
            Debug.LogError("Item is null!");
            continue;
        }

        Debug.Log($"Initializing item: {item.itemName}, Type: {item.itemType}");
            //dynamically choose the correct container based on the item type/category
            Transform targetContainer = item.itemType switch
            {
                ItemType.Car => carItemContainer,
                ItemType.CarSkin => carSkinItemContainer,
                ItemType.TurretSkin => turretSkinItemContainer,
                _ => shopItemContainer // Default or backup container if type not matched
            };

            GameObject newItemUI = Instantiate(shopItemPrefab, targetContainer);
            ShopItemUI itemUI = newItemUI.GetComponent<ShopItemUI>();

            // Set the item UI details (name, cost, and icon)
            itemUI.SetItemDetails(item);
        }
    }

    // Update is called once per frame
    public void UpdateItemStatus(ShopItem item)
    {
        foreach (Transform itemUI in shopItemContainer)
        {
            ShopItemUI shopItemUI = itemUI.GetComponent<ShopItemUI>();
            if (shopItemUI.GetItem() == item)
            {
                shopItemUI.SetPurchased();
            }
        }
    }

    public void ShowCarItems()
    {
        carItemContainer.gameObject.SetActive(true);
        carSkinItemContainer.gameObject.SetActive(false);
        turretSkinItemContainer.gameObject.SetActive(false);

        PopulatePanel(ItemType.Car);
    }

    public void ShowCarSkinItems()
    {
        carItemContainer.gameObject.SetActive(false);
        carSkinItemContainer.gameObject.SetActive(true);
        turretSkinItemContainer.gameObject.SetActive(false);

        PopulatePanel(ItemType.CarSkin);
    }

    public void ShowTurretSkinItems()
    {
        carItemContainer.gameObject.SetActive(false);
        carSkinItemContainer.gameObject.SetActive(false);
        turretSkinItemContainer.gameObject.SetActive(true);

        PopulatePanel(ItemType.TurretSkin);
    }

    private void PopulatePanel(ItemType type)
    {

        Transform container = type switch
        {
            ItemType.Car => carItemContainer,
            ItemType.CarSkin => carSkinItemContainer,
            ItemType.TurretSkin => turretSkinItemContainer,
            _ => null
        };

        // Check if the container is null
        if (container == null)
        {
            Debug.LogError($"Container for {type} is null.");
            return;
        }

        // Clear the existing items in the container
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        // Populate the container with the correct items
        foreach (ShopItem item in itemsForSale)
        {
            if (item == null)
            {
                Debug.LogError("Null item in itemsForSale!");
                continue;
            }


            if (item.itemType == type)
            {
                // Check if the shopItemPrefab is set
                if (shopItemPrefab == null)
                {
                    Debug.LogError("Shop item prefab is not assigned.");
                    return;
                }

                GameObject newItemUI = Instantiate(shopItemPrefab, container);
                ShopItemUI itemUI = newItemUI.GetComponent<ShopItemUI>();
                itemUI.SetItemDetails(item);

                // Check if itemUI is assigned correctly
                if (itemUI == null)
                {
                    Debug.LogError("ShopItemUI component is missing on the instantiated prefab.");
                    continue; // Skip to the next item if itemUI is null
                }

            }
        }
    }



}
