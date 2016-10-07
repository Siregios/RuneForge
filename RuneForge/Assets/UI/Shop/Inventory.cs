using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
    //public Dictionary<string, Item> inventoryDict = new Dictionary<string, Item>();
    public Dictionary<string, int> inventoryDict = new Dictionary<string, int>();

    public Inventory()
    {
        //Money
        AddItem("Money", 0);

        foreach (Item item in ItemCollection.allItems)
        {
            AddItem(item.name, 0);
        }
        ////Basic Materials
        //foreach (string material in ItemCollection.materialList)
        //    AddItem(material, 0);

        ////Runes
        //foreach (string rune in AllItems.runeList)
        //    AddItem(rune, 0);
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

    //void CreateItem(string item)
    //{
    //    inventoryDict.Add(item, new Item(item, 0, 0f));
    //}

    //void CreateRune(string rune)
    //{
    //    foreach (char rank in Rune.runeRanks)
    //    {
    //        CreateItem(rune + rank);
    //    }
    //}
}