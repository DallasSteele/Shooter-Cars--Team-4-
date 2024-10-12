using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

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


    //purchase logic
    public void PurchaseItem(ShopItem item)
    {
        if (CoinManager.Instance.GetCurrentCoins() >= item.cost && !PlayerInventory.Instance.HasItem(item.itemName))
        {
            CoinManager.Instance.SpendCoins(item.cost);
            PlayerInventory.Instance.AddItem(item.itemName);
            ShopUI.Instance.UpdateItemStatus(item);

            // If the purchased item is a CarSkin, apply it
            if (item.itemType == ItemType.CarSkin)
            {
                Material skinMaterial = SkinDatabase.Instance.GetSkinMaterial(item.itemName);
                if (skinMaterial != null)
                {
                    CarSkinManager carSkinManager = FindObjectOfType<CarSkinManager>();
                    carSkinManager.ApplySkin(skinMaterial);
                    carSkinManager.SaveSkin(item.itemName);
                }
            }
        }
        else
        {
            Debug.Log("Not enough coins or item already owned!");
        }
    }



}