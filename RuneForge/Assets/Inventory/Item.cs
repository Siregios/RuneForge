using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Item
{
    [XmlIgnoreAttribute]
    public static int maxAttributeLevel = 10;

    [XmlAttribute("name")]
    public string name;

    [XmlElement("Price")]
    public int price;

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
    public Dictionary<string, int> providedAttributes;

    [XmlIgnoreAttribute]
    public Dictionary<string, int> recipe;

    [XmlIgnoreAttribute]
    public Dictionary<string, int> requiredAttributes;

    [XmlElement("Level")]
    public int level = 0;

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
        this.providedAttributes = new Dictionary<string, int>(copyItem.providedAttributes);
        this.recipe = new Dictionary<string, int>(copyItem.recipe);
        this.minigamesRequired = copyItem.minigamesRequired;
        this.requiredAttributes = new Dictionary<string, int>(copyItem.minigamesRequired);
        this.icon = copyItem.icon;
        this.level = copyItem.level;
    }
}