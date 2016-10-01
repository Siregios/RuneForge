using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Inventory {
    public static Dictionary<string, int> inventoryDict = new Dictionary<string, int>();
    public static List<string> materialList, runeList;

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
        inventoryDict[item]++;
    }

    public static void AddItem(string item, int count)
    {
        inventoryDict[item] = count;
    }

    public static int GetItemCount(string item)
    {
        return inventoryDict[item];
    }

    static void CreateItem(string item)
    {
        inventoryDict.Add(item, 0);
    }

    static void CreateRune(string rune)
    {
        CreateItem(rune + "S");
        CreateItem(rune + "A");
        CreateItem(rune + "B");
        CreateItem(rune + "C");
    }
}