using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Item
{
    [XmlAttribute("name")]
    public string name;

    //[XmlElement("Type")]
    //public string type;

    [XmlElement("Price")]
    public int price;

    //[XmlElement("isIngredient")]
    //public bool isIngredient;

    //[XmlElement("isProduct")]
    //public bool isProduct;
    [XmlElement("Class")]
    public string Class;

    [XmlElement("IngredientType")]
    public string ingredientType;

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
        this.price = copyItem.price;
        this.Class = copyItem.Class;
        this.ingredientType = copyItem.ingredientType;
        this.providedAttributes = copyItem.providedAttributes;
        this.recipe = copyItem.recipe;
        this.minigamesRequired = copyItem.minigamesRequired;
        this.requiredAttributes = copyItem.requiredAttributes;
        this.icon = copyItem.icon;
    }
}