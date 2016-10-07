using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public static class ItemCollection{
    public static MaterialCollection materialCollection = MaterialCollection.Load();
    public static RuneCollection runeCollection = RuneCollection.Load();
    public static List<Item> allItems = new List<Item>(), materialList = new List<Item>(), runeList = new List<Item>();
    public static string runeRanks = "SABC";

    static ItemCollection()
    {
        materialList = materialCollection.materials;
        runeList = runeCollection.runes;

        foreach (Item item in materialList)
            allItems.Add(item);
        foreach (Item item in runeList)
            allItems.Add(item);
    }
}

[XmlRoot("MaterialCollection")]
public class MaterialCollection
{
    [XmlArray("MaterialList")]
    [XmlArrayItem("Material")]
    public List<Item> materials = new List<Item>();

    public static MaterialCollection Load()
    {
        TextAsset materialsXml = Resources.Load<TextAsset>("ItemData/Materials");

        XmlSerializer serializer = new XmlSerializer(typeof(MaterialCollection));

        StringReader reader = new StringReader(materialsXml.text);

        MaterialCollection materials = serializer.Deserialize(reader) as MaterialCollection;

        reader.Close();

        return materials;
    }
}

[XmlRoot("RuneCollection")]
public class RuneCollection
{
    [XmlArray("RuneList")]
    [XmlArrayItem("Rune")]
    public List<Item> runes = new List<Item>();

    public static RuneCollection Load()
    {
        TextAsset runesXml = Resources.Load<TextAsset>("ItemData/Runes");

        XmlSerializer serializer = new XmlSerializer(typeof(RuneCollection));

        StringReader reader = new StringReader(runesXml.text);

        RuneCollection runes = serializer.Deserialize(reader) as RuneCollection;

        reader.Close();

        return runes;
    }
}