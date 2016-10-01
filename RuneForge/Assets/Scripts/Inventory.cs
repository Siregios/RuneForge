using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Inventory {
    public static Dictionary<string, int> inventoryDict = new Dictionary<string, int>();

    static Inventory()
    {
        //Money
        CreateItem("Money");

        //Basic Materials
        CreateItem("OakWood");
        CreateItem("CopperBar");
        CreateItem("Bone");
        CreateItem("Scale");
        CreateItem("Clay");
        CreateItem("Chalk");

        //Runes
        CreateRune("Fire");
        CreateRune("Water");
        CreateRune("Air");
        CreateRune("Earth");
        CreateRune("MagnetPlus");
        CreateRune("MagnetMinus");
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

    //public static Dictionary<Rune, int[]> runeCount = new Dictionary<Rune, int[]>() {
    //    { Rune.Fire, new int[4] {0, 0, 0, 0} },
    //    { Rune.Water, new int[4] {0, 0, 0, 0} },
    //    { Rune.Air, new int[4] {0, 0, 0, 0} },
    //    { Rune.Earth, new int[4] {0, 0, 0, 0} },
    //    { Rune.MagnetPlus, new int[4] {0, 0, 0, 0} },
    //    { Rune.MagnetMinus, new int[4] {0, 0, 0, 0} },
    //};
}


//// Add this to some GameObject in the very first scene so that the Inventory can be properly initialized
//public class InventoryManager : MonoBehaviour
//{
//    void Awake()
//    {

//    }
//}