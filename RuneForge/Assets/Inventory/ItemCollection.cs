using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public static class ItemCollection{
    static Items items = XmlReader<Items>.Load("XMLData/Items");

    public static List<Item> itemList = new List<Item>();
    public static Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

    static ItemCollection()
    {
        foreach (Item item in items.items)
        {
            item.icon = Resources.Load<Sprite>("ItemSprites/" + item.name);

            if ((item.Class == "Ingredient" || item.Class == "Rune") && item.provtAttrStr != null)
            {
                foreach (string pairString in item.provtAttrStr.Trim().Split(','))
                {
                    var pair = pairString.Trim().Split(':');
                    item.providedAttributes.Add(pair[0].Trim(), int.Parse(pair[1].Trim()));
                }
            }

            if (item.Class == "Product" || item.Class == "Rune")
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
    }

    //This funciton might be slow when there are lots of items in the game.
    public static List<Item> FilterItemList(string filter)
    {
        string lowerFilter = filter.Trim().ToLower();
        if (lowerFilter.Contains("all"))
            return itemList;

        return FilterSpecificList(itemList, filter);
    }

    public static List<Item> FilterSpecificList(List<Item> specificList, string filter)
    {
        string lowerFilter = filter.Trim().ToLower();

        List<Item> result = new List<Item>();

        foreach (Item item in specificList)
        {
            if (lowerFilter.Contains("ingredient") && item.Class == "Ingredient")
                result.Add(item);
            else if (lowerFilter.Contains("product") && (item.Class == "Product" || item.Class == "Rune"))
                result.Add(item);
            else if (lowerFilter.Contains("rune") && item.Class == "Rune")
                result.Add(item);
            else if (item.name.ToLower().Contains(lowerFilter) || item.ingredientType.ToLower().Contains(lowerFilter))
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