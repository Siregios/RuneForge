using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
    public Dictionary<string, int> inventoryDict = new Dictionary<string, int>();

    public Inventory()
    {        
        foreach (Item item in ItemCollection.itemList)
        {
            SetItemCount(item.name, 0);
        }
    }

    //Increment the item count by 1
    public void AddItem(string itemName)
    {
        if (isInfinite(itemName))
            return;
        inventoryDict[itemName]++;
    }

    //Increment the item count by a specific count
    public void AddItem(string itemName, int count)
    {
        if (isInfinite(itemName))
            return;
        inventoryDict[itemName] += count;
    }

    //Decrement the item count by 1
    public void SubtractItem(string itemName)
    {
        if (isInfinite(itemName))
            return;
        inventoryDict[itemName]--;
    }

    //Decrement the item count by a specific count
    public void SubtractItem(string itemName, int count)
    {
        if (isInfinite(itemName))
            return;
        inventoryDict[itemName] -= count;
    }

    //Set the item count to a specific count
    public void SetItemCount(string itemName, int count)
    {
        if (isInfinite(itemName))
            return;
        inventoryDict[itemName] = count;
    }

    public int GetItemCount(string itemName)
    {
        return inventoryDict[itemName];
    }

    public bool isInfinite(string itemName)
    {
        return inventoryDict.ContainsKey(itemName) && inventoryDict[itemName] == int.MaxValue;
    }
}