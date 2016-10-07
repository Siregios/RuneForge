using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
    public Dictionary<string, int> inventoryDict = new Dictionary<string, int>();

    public Inventory()
    {
        //Money
        AddItem("Money", 0);

        foreach (Item item in ItemCollection.itemList)
        {
            AddItem(item.name, 0);
        }
    }

    public void AddItem(string item)
    {
        inventoryDict[item]++;
    }

    public void AddItem(string item, int count)
    {
        inventoryDict[item] = count;
    }

    public int GetItemCount(string item)
    {
        return inventoryDict[item];
    }
}