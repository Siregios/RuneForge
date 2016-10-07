using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Item
{
    [XmlAttribute("name")]
    public string name;
    
    [XmlElement("Price")]
    public float price;

    //public Item()
    //{
    //    this.name = "";
    //    //this.count = 0;
    //    this.price = 0f;
    //    image = null;
    //}

    //public Item(string name, float price)
    //{
    //    this.name = name;
    //    //this.count = count;
    //    this.price = price;
    //    this.image = Resources.Load<Sprite>("ItemSprites/" + name);
    //}
}

//public class Rune
//{
//    [XmlElement("")]
//}

//public static class AllItems
//{
//    public static List<string> materialList = new List<string>() { "OakWood", "CopperBar", "Bone", "Scale", "Clay", "Chalk" };
//    public static List<string> runeList = new List<string>() { "Fire", "Water", "Air", "Earth", "MagnetPlus", "MagnetMinus" };
//    public static string runeRanks = "SABC";
//    public static Dictionary<string, Item> itemDictionary = new Dictionary<string, Item>();

//    static AllItems()
//    {
//        List<string> newRuneList = new List<string>();
//        foreach (string runeType in runeList)
//        {
//            foreach (char rank in runeRanks)
//            {
//                newRuneList.Add(runeType + rank);
//            }
//        }

//        runeList = newRuneList;
//    }
//}