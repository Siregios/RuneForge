using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public static class ItemCollection{
    static Items items = XmlReader<Items>.Load("ItemData/Items");

    public static List<Item> itemList = new List<Item>();
    //public static List<Item> ingredientList = new List<Item>();
    //public static List<Item> productList = new List<Item>();

    ////Might not need these?
    //public static List<Item> materialList = new List<Item>();
    //public static List<Item> runeList = new List<Item>();

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
                    item.providedAttributes.Add(pair[0].Trim(), int.Parse(pair[1].Trim()));
                }
            }

            if (item.isProduct)
            {
                foreach (string pairString in item.recipeString.Trim().Split(','))
                {
                    var pair = pairString.Trim().Split(':');
                    item.recipe.Add(pair[0].Trim(), int.Parse(pair[1].Trim()));
                }

                foreach (string pairString in item.reqAttrStr.Trim().Split(','))
                {
                    var pair = pairString.Trim().Split(':');
                    item.requiredAttributes.Add(pair[0].Trim(), int.Parse(pair[1].Trim()));
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

    //This funciton might be slow when there are lots of items in the game.
    public static List<Item> FilterItem(string filter)
    {
        string lowerFilter = filter.Trim().ToLower();

        if (lowerFilter.Contains("all"))
            return itemList;

        List<Item> result = new List<Item>();

        foreach (Item item in itemList)
        {
            if (lowerFilter.Contains("ingredient") && item.isIngredient)
                result.Add(item);
            else if (lowerFilter.Contains("product") && item.isProduct)
                result.Add(item);
            else if (item.name.ToLower().Contains(lowerFilter) || item.type.ToLower().Contains(lowerFilter))
                result.Add(item);
        }

        return result;
    }
}

[XmlRoot("AllItems")]
public class Items
{
    [XmlArray("ItemList")]
    [XmlArrayItem("Item")]
    public List<Item> items = new List<Item>();
}