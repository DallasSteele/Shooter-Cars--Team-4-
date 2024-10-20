using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemCostText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button buyButton;

    private ShopItem currentItem;

    // Set the details of each shop item
    public void SetItemDetails(ShopItem item)
    {
        currentItem = item;
        itemNameText.text = item.itemName;
        itemCostText.text = $"Cost: {item.cost}";
        itemIcon.sprite = item.itemIcon;

        UpdateUIState();
    }

    // Update the UI based on item state (Bought/Equipped/Not Owned)
    private void UpdateUIState()
    {
        if (PlayerInventory.Instance.HasItem(currentItem.itemName))
        {
            if (PlayerInventory.Instance.IsEquipped(currentItem.itemName))
            {
                SetEquippedState(); // Item is equipped
            }
            else
            {
                SetEquipState(); // Item is owned but not equipped
            }
        }
        else
        {
            SetBuyState(); // Item is not owned
        }
    }

    // Set the button and status for buying an item
    private void SetBuyState()
    {
        SetItemStatus("Buy");
        EnableBuyButton();
        SetBuyButtonAction(() =>
        {
            ShopManager.Instance.PurchaseItem(currentItem);
            UpdateUIState(); // Refresh UI after purchase
        });
    }

    // Set the button and status for equipping an item
    private void SetEquipState()
    {
        SetItemStatus("Equip");
        EnableBuyButton();
        SetEquipButtonAction(() =>
        {
            PlayerInventory.Instance.EquipItem(currentItem.itemName);
            UpdateUIState(); // Refresh UI after equipping
        });
    }

    // Set the button and status for an equipped item
    private void SetEquippedState()
    {
        SetItemStatus("Equipped");
        DisableBuyButton(); // Disable the button since item is equipped
    }

    public void SetItemStatus(string status)
    {
        statusText.text = status;
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = status; // Update button text
    }

    public void EnableBuyButton()
    {
        buyButton.interactable = true;
    }

    public void DisableBuyButton()
    {
        buyButton.interactable = false;
    }

    public void SetBuyButtonAction(UnityEngine.Events.UnityAction action)
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(action);
    }

    public void SetEquipButtonAction(UnityEngine.Events.UnityAction action)
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(action);
    }

    // Restored: Get the current item associated with this UI element
    public ShopItem GetItem()
    {
        return currentItem;
    }

    // Restored: Mark the item as purchased
    public void SetPurchased()
    {
        SetItemStatus("Owned");
        DisableBuyButton(); // Disable the button since item is bought
    }

}

