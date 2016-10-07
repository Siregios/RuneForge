using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public static class ItemCollection{
    public static MaterialCollection materialCollection = XmlReader<MaterialCollection>.Load("ItemData/Materials");
    public static RuneCollection runeCollection = XmlReader<RuneCollection>.Load("ItemData/Runes");
    public static List<Item> itemList = new List<Item>(), materialList = new List<Item>();
    public static List<Rune> runeList = new List<Rune>();
    public static Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

    static ItemCollection()
    {
        materialList = materialCollection.materials;

        foreach (Item item in runeCollection.runes)
        {
            foreach(char rank in Rune.runeRanks.Keys)
                runeList.Add(new Rune(item, rank));
        }

        foreach (Item material in materialList)
            itemDict.Add(material.name, material);
        foreach (Rune rune in runeList)
            itemDict.Add(rune.name, rune);
    }
}

public static class XmlReader<T>
{
    public static T Load(string path)
    {
        TextAsset Xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(Xml.text);
        T result = (T)serializer.Deserialize(reader);
        reader.Close();
        return result;
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
    public List<Item> runes = new List<Item>();
}