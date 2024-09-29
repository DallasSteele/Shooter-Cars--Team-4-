using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Car,
    CarSkin,
    TurretSkin
}

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public int cost;
    public Sprite itemIcon; //ui display, null is possible
    public bool isConsumable; //true if the item is one-time use {CHECK IF ITS ONE TIME USE}
    public ItemType itemType; //determines if its either one of the 3
}
