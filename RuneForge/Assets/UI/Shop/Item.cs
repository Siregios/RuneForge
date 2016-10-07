using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Item
{
    [XmlAttribute("name")]
    public string name;
    
    [XmlElement("Price")]
    public int price;

    public Item()
    {
        this.name = "";
        this.price = 0;
    }

    public Item(Item copyItem)
    {
        this.name = copyItem.name;
        this.price = copyItem.price;
    }
}

public class Rune : Item
{
    public static Dictionary<char, float> runeRanks = new Dictionary<char, float>()
    {
        { 'S', 200.0f },
        { 'A', 150.0f },
        { 'B', 95.0f },
        { 'C', 75.0f }
    };

    public char rank;

    public Rune(Item item, char rank) : base(item)
    {
        base.name += rank;
        base.price *= Mathf.CeilToInt(runeRanks[rank] / 100f);
        this.rank = rank;
    }
}