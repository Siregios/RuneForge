using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item
{
    public string name;
    public int count;
    public float price;
    public Item(string name, int count, float price)
    {
        this.name = name;
        this.count = count;
        this.price = price;
    }
}

public static class Inventory {
    public static Dictionary<string, Item> inventoryDict = new Dictionary<string, Item>();
    public static List<string> materialList, runeList;
    public static string runeRanks = "SABC";

    static Inventory()
    {
        //Money
        CreateItem("Money");

        //Basic Materials
        materialList = new List<string>() { "OakWood", "CopperBar", "Bone", "Scale", "Clay", "Chalk" };
        foreach (string material in materialList)
            CreateItem(material);

        //Runes
        runeList = new List<string>() { "Fire", "Water", "Air", "Earth", "MagnetPlus", "MagnetMinus"};
        foreach (string rune in runeList)
            CreateRune(rune);
    }

    public static void AddItem(string item)
    {
        inventoryDict[item].count++;
    }

    public static void AddItem(string item, int count)
    {
        inventoryDict[item].count = count;
    }

    public static int GetItemCount(string item)
    {
        return inventoryDict[item].count;
    }

    static void CreateItem(string item)
    {
        inventoryDict.Add(item, new Item(item, 0, 0f));
    }

    static void CreateRune(string rune)
    {
        foreach (char rank in runeRanks)
        {
            CreateItem(rune + rank);
        }
    }
}