using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public static class ItemCollection
{
    static Items items = XmlReader<Items>.Load("XMLData/Items");

    public static List<Item> itemList = new List<Item>();
    public static Dictionary<string, Item> itemDict = new Dictionary<string, Item>();

    static ItemCollection()
    {
        foreach (Item item in items.items)
        {
            item.icon = Resources.Load<Sprite>("ItemSprites/" + item.name);

            if (/*(item.Class == "Ingredient" || item.Class == "Rune") && */item.provtAttrStr != null)
            {
                item.providedAttributes.Add("Fire", 0);
                item.providedAttributes.Add("Water", 0);
                item.providedAttributes.Add("Earth", 0);
                item.providedAttributes.Add("Air", 0);

                foreach (string pairString in item.provtAttrStr.Trim().Split(','))
                {
                    var pair = pairString.Trim().Split(':');
                    string attribute = pair[0].Trim();
                    int value = int.Parse(pair[1].Trim());
                    if (attribute == "ALL")
                    {
                        item.providedAttributes["Fire"] = value;
                        item.providedAttributes["Water"] = value;
                        item.providedAttributes["Earth"] = value;
                        item.providedAttributes["Air"] = value;
                    }
                    else
                    {
                        item.providedAttributes[attribute] = int.Parse(pair[1].Trim());
                    }
                }
            }


            if (item.recipeString != null)
            {
                foreach (string pairString in item.recipeString.Trim().Split(','))
                {
                    var pair = pairString.Trim().Split(':');
                    item.recipe.Add(pair[0].Trim(), int.Parse(pair[1].Trim()));
                }
            }

            if (item.reqAttrStr != null)
            {
                item.requiredAttributes.Add("Fire", 0);
                item.requiredAttributes.Add("Water", 0);
                item.requiredAttributes.Add("Earth", 0);
                item.requiredAttributes.Add("Air", 0);

                foreach (string pairString in item.reqAttrStr.Trim().Split(','))
                {
                    var pair = pairString.Trim().Split(':');
                    item.requiredAttributes[pair[0].Trim()] = int.Parse(pair[1].Trim());
                }
            }
            

            itemList.Add(item);
            itemDict.Add(item.name, item);

            if (item.Class == "Product" || item.Class == "Rune")
            {
                CreateImprovedProducts(item);
            }
        }
    }

    static void CreateImprovedProducts(Item product)
    {
        Item highQuality = new Item(product);
        highQuality.name = string.Format("{0} (HQ)", product.name);
        highQuality.price *= 2;
        highQuality.Class = "ImprovedProduct";
        foreach (var kvp in product.providedAttributes)
        {
            if (highQuality.providedAttributes[kvp.Key] > 0)
                highQuality.providedAttributes[kvp.Key] = kvp.Value + 1;
        }

        Item masterCraft = new Item(product);
        masterCraft.name = string.Format("{0} (MC)", product.name);
        masterCraft.price *= 3;
        masterCraft.Class = "ImprovedProduct";
        foreach (var kvp in product.providedAttributes)
        {
            if (masterCraft.providedAttributes[kvp.Key] > 0)
                masterCraft.providedAttributes[kvp.Key] = kvp.Value + 2;
        }

        itemList.Add(highQuality);
        itemDict.Add(highQuality.name, highQuality);
        itemList.Add(masterCraft);
        itemDict.Add(masterCraft.name, masterCraft);
    }

    //This funciton might be slow when there are lots of items in the game.
    public static List<Item> FilterItemList(string filter)
    {
        string lowerFilter = filter.Trim().ToLower();

        return FilterSpecificList(itemList, filter);
    }

    public static List<Item> FilterSpecificList(List<Item> specificList, string filter)
    {
        string lowerFilter = filter.Trim().ToLower().Replace(" ", string.Empty);

        List<Item> result = new List<Item>();

        foreach (Item item in specificList)
        {
            if (item.level <= MasterGameManager.instance.playerStats.level)
            {
                if (lowerFilter == "all")
                    result.Add(item);
                else if (lowerFilter.Contains("ingredient") && (item.Class == "Ingredient" || item.ingredientType.Contains("Rune")))
                    result.Add(item);
                else if (lowerFilter.Contains("baseproduct") && (item.Class == "Product" || item.Class == "Rune"))
                    result.Add(item);
                else if (lowerFilter.Contains("improvedproduct") && (item.Class == "ImprovedProduct"))
                    result.Add(item);
                else if (lowerFilter == "product" && (item.Class == "Product" || item.Class == "Rune" || item.Class == "ImprovedProduct"))
                    result.Add(item);
                else if (lowerFilter.Contains("material") && item.Class == "Ingredient")
                    result.Add(item);
                else if (lowerFilter == "rune" && item.ingredientType.Contains("Rune"))
                    result.Add(item);
                else if (item.name.ToLower().Replace(" ", string.Empty).Contains(lowerFilter) || item.ingredientType.ToLower().Replace(" ", string.Empty).Contains(lowerFilter))
                    result.Add(item);
            }
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