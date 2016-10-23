﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public static class ItemCollection{
    //static MaterialCollection materialCollection = XmlReader<MaterialCollection>.Load("ItemData/Materials");
    //static RuneCollection runeCollection = XmlReader<RuneCollection>.Load("ItemData/Runes");
    //static ProductCollection productCollection = XmlReader<ProductCollection>.Load("ItemData/Products");
    static Items items = XmlReader<Items>.Load("ItemData/Items");

    public static List<Item> itemList = new List<Item>();
    public static List<Item> ingredientList = new List<Item>();
    public static List<Item> productList = new List<Item>();

    //Might not need these?
    public static List<Item> materialList = new List<Item>();
    public static List<Item> runeList = new List<Item>();

    public static Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

    static ItemCollection()
    {
        foreach (Item item in items.items)
        {
            item.icon = Resources.Load<Sprite>("ItemSprites/" + item.name);

            if (item.isIngredient)
            {
                foreach (string pairString in item.provtAttrStr.Trim().Split(','))
                {
                    var pair = pairString.Trim().Split(':');
                    item.providedAttributes.Add(pair[0], int.Parse(pair[1]));
                }
            }

            if (item.isProduct)
            {
                foreach (string pairString in item.recipeString.Trim().Split(','))
                {
                    var pair = pairString.Trim().Split(':');
                    item.recipe.Add(pair[0], int.Parse(pair[1]));
                }

                foreach (string pairString in item.reqAttrStr.Trim().Split(','))
                {
                    var pair = pairString.Trim().Split(':');
                    item.requiredAttributes.Add(pair[0], int.Parse(pair[1]));
                }
            }

            itemList.Add(item);
            itemDict.Add(item.name, item);
        }
        //materialList = materialCollection.materials;
        //runeList = runeCollection.runes;

        //foreach (Product rune in runeList)
        //{
        //    productList.Add(rune);
        //}

        //foreach (Item material in materialList)
        //{
        //    material.icon = Resources.Load<Sprite>("ItemSprites/" + material.name);
        //    itemList.Add(material);
        //    itemDict.Add(material.name, material);
        //}
        //foreach (Rune rune in runeList)
        //{
        //    rune.icon = Resources.Load<Sprite>("ItemSprites/" + rune.name);
        //    itemList.Add(rune);
        //    itemDict.Add(rune.name, rune);
        //}
    }
}

[XmlRoot("AllItems")]
public class Items
{
    [XmlArray("ItemList")]
    [XmlArrayItem("Item")]
    public List<Item> items = new List<Item>();
}

//[XmlRoot("MaterialCollection")]
//public class MaterialCollection
//{
//    [XmlArray("MaterialList")]
//    [XmlArrayItem("Material")]
//    public List<Item> materials = new List<Item>();
//}

//[XmlRoot("ProductCollection")]
//public class ProductCollection
//{
//    [XmlArray("Productlist")]
//    [XmlArrayItem("Product")]
//    public List<Product> products = new List<Product>();
//}

//[XmlRoot("RuneCollection")]
//public class RuneCollection
//{
//    [XmlArray("RuneList")]
//    [XmlArrayItem("Rune")]
//    public List<Rune> runes = new List<Rune>();
//}