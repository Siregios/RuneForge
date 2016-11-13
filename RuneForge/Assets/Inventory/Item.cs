using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Item
{
    [XmlAttribute("name")]
    public string name;

    [XmlElement("Type")]
    public string type;

    [XmlElement("Price")]
    public int price;

    [XmlElement("isIngredient")]
    public bool isIngredient;

    [XmlElement("isProduct")]
    public bool isProduct;

    [XmlElement("ProvidedAttributes")]
    public string provtAttrStr;

    [XmlElement("Recipe")]
    public string recipeString;

    [XmlElement("MinigamesRequired")]
    public int minigamesRequired;

    [XmlElement("RequiredAttributes")]
    public string reqAttrStr;

    [XmlIgnoreAttribute]
    public static int maxAttributeLevel = 10;

    [XmlIgnoreAttribute]
    public Dictionary<string, int> providedAttributes;

    [XmlIgnoreAttribute]
    public Dictionary<string, int> recipe;

    [XmlIgnoreAttribute]
    public Dictionary<string, int> requiredAttributes;

    public Sprite icon;

    public Item()
    {
        providedAttributes = new Dictionary<string, int>();
        recipe = new Dictionary<string, int>();
        requiredAttributes = new Dictionary<string, int>();
    }

    public Item(Item copyItem)
    {
        this.name = copyItem.name;
        this.type = copyItem.type;
        this.price = copyItem.price;
        this.isIngredient = copyItem.isIngredient;
        this.isProduct = copyItem.isProduct;
        this.providedAttributes = copyItem.providedAttributes;
        this.recipe = copyItem.recipe;
        this.minigamesRequired = copyItem.minigamesRequired;
        this.requiredAttributes = copyItem.requiredAttributes;
        this.icon = copyItem.icon;
    }
}

//public class Rune : Product
//{
//    public bool isMaster = false;

//    public Rune() : base() { }

//    public Rune(Product product) : base(product)
//    {
//        if (this.name.Substring(this.name.Length - 2) == "MC")
//            isMaster = true;
//    }
//}

//public class Product : Item
//{
//    [XmlElement("Attribute1")]
//    public int attribute1;

//    public Product() : base() { }

//    public Product(Item item) : base(item) { }
//}