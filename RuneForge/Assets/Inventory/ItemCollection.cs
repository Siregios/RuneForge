using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public static class ItemCollection{
    static MaterialCollection materialCollection = XmlReader<MaterialCollection>.Load("ItemData/Materials");
    static RuneCollection runeCollection = XmlReader<RuneCollection>.Load("ItemData/Runes");

    public static List<Item> itemList = new List<Item>();
    public static List<Item> materialList = new List<Item>();
    public static List<Rune> runeList = new List<Rune>();

    public static Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

    static ItemCollection()
    {
        materialList = materialCollection.materials;
        runeList = runeCollection.runes;

        //foreach (Item item in runeCollection.runes)
        //{
        //    foreach(char rank in Rune.runeRanks.Keys)
        //        runeList.Add(new Rune(item, rank));
        //}

        foreach (Item material in materialList)
        {
            material.icon = Resources.Load<Sprite>("ItemSprites/" + material.name);
            itemList.Add(material);
            itemDict.Add(material.name, material);
        }
        foreach (Rune rune in runeList)
        {
            rune.icon = Resources.Load<Sprite>("ItemSprites/" + rune.name);
            itemList.Add(rune);
            itemDict.Add(rune.name, rune);
        }
    }
}

[XmlRoot("MaterialCollection")]
public class MaterialCollection
{
    [XmlArray("MaterialList")]
    [XmlArrayItem("Material")]
    public List<Item> materials = new List<Item>();
}

[XmlRoot("RuneCollection")]
public class RuneCollection
{
    [XmlArray("RuneList")]
    [XmlArrayItem("Rune")]
    public List<Rune> runes = new List<Rune>();
}