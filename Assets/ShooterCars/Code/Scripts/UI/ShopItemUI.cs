using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemCostText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button buyButton;

    private ShopItem currentItem;
    private CarController carController;  //ref to the players car controller

    //set the details of each item
    public void SetItemDetails(ShopItem item)
    {
        currentItem = item;
        itemNameText.text = item.itemName;
        itemCostText.text = $"Cost: {item.cost}";
        itemIcon.sprite = item.itemIcon;
        //carController = carControllerRef; //the car controller reference

        //check if the item is already purchased
        if (PlayerInventory.Instance.HasItem(item.itemName))
        {
            SetPurchased();
        }
        else
        {
            buyButton.onClick.AddListener(() => ShopManager.Instance.PurchaseItem(item) );
        }
    }

    //mark the item as purchased in the UI
    public void SetPurchased()
    {
        buyButton.interactable = false;
        itemCostText.text = "Owned";
    }

    public ShopItem GetItem()
    {
        return currentItem;
    }
}
