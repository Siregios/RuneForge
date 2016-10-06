using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item
{
    public string name;
    public int count;
    public float price;

    public static List<string> materialList = new List<string>() { "OakWood", "CopperBar", "Bone", "Scale", "Clay", "Chalk" };
    public static List<string> runeList = new List<string>() { "Fire", "Water", "Air", "Earth", "MagnetPlus", "MagnetMinus"};

    public Item()
    {
        this.name = "";
        this.count = 0;
        this.price = 0f;
    }

    public Item(string name, int count, float price)
    {
        this.name = name;
        this.count = count;
        this.price = price;
    }
}

public class Rune : Item
{
    public static string runeRanks = "SABC";
}