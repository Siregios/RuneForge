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

    //Increment the item count by 1
    public void AddItem(Item item)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item]++;
        //if (!inventoryList.Contains(item))
        //    inventoryList.Add(item);
    }

    //Increment the item count by a specific count
    public void AddItem(Item item, int count)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item] += count;
        //if (!inventoryList.Contains(item))
        //    inventoryList.Add(item);
    }

    //Decrement the item count by 1
    public void SubtractItem(Item item)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item]--;
        //if (inventoryDict[item] <= 0)
        //    inventoryList.Remove(item);
    }

    //Decrement the item count by a specific count
    public void SubtractItem(Item item, int count)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item] -= count;
        //if (inventoryDict[item] <= 0)
        //    inventoryList.Remove(item);
    }

    //Set the item count to a specific count
    public void SetItemCount(Item item, int count)
    {
        if (isInfinite(item))
            return;
        inventoryDict[item] = count;
        //if (count > 0 && !inventoryList.Contains(item))
        //    inventoryList.Add(item);
        //if (count <= 0 && inventoryList.Contains(item))
        //    inventoryList.Remove(item);
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