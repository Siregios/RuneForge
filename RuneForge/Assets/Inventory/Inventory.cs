using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory
{
    public enum InventoryType
    {
        PLAYER,
        SHOP
    }

    public Dictionary<Item, int> inventoryDict = new Dictionary<Item, int>();
    //public List<Item> inventoryList = new List<Item>();

    public Inventory()
    {
        foreach (Item item in ItemCollection.itemList)
        {
            SetItemCount(item, 0);
        }
    }

    public Inventory(Inventory copyInventory)
    {
        this.inventoryDict = new Dictionary<Item, int>(copyInventory.inventoryDict);
    }

    //Increment the item count by 1
    public void AddItem(Item item)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item]++;
    }

    //Increment the item count by a specific count
    public void AddItem(Item item, int count)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item] += count;
    }

    //Decrement the item count by 1
    public void SubtractItem(Item item)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item]--;
    }

    //Decrement the item count by a specific count
    public void SubtractItem(Item item, int count)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item] -= count;
    }

    //Set the item count to a specific count
    public void SetItemCount(Item item, int count)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item] = count;
    }

    public int GetItemCount(Item item)
    {
        return inventoryDict[item];
    }

    public bool isInfinite(Item item)
    {
        return inventoryDict.ContainsKey(item) && inventoryDict[item] == int.MaxValue;
    }
}