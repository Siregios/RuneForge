using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
    public Dictionary<string, Item> inventoryDict = new Dictionary<string, Item>();

    public Inventory()
    {
        //Money
        CreateItem("Money");

        //Basic Materials
        foreach (string material in Item.materialList)
            CreateItem(material);

        //Runes
        foreach (string rune in Item.runeList)
            CreateRune(rune);
    }

    public void AddItem(string item)
    {
        inventoryDict[item].count++;
    }

    public void AddItem(string item, int count)
    {
        inventoryDict[item].count = count;
    }

    public int GetItemCount(string item)
    {
        return inventoryDict[item].count;
    }

    void CreateItem(string item)
    {
        inventoryDict.Add(item, new Item(item, 0, 0f));
    }

    void CreateRune(string rune)
    {
        foreach (char rank in Rune.runeRanks)
        {
            CreateItem(rune + rank);
        }
    }
}