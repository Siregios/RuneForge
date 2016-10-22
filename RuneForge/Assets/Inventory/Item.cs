using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Item
{
    [XmlAttribute("name")]
    public string name;
    
    [XmlElement("Price")]
    public int price;

    public Sprite icon;

    public Item()
    {
        this.name = "";
        this.price = 0;
    }

    public Item(Item copyItem)
    {
        this.name = copyItem.name;
        this.price = copyItem.price;
        this.icon = copyItem.icon;
    }
}

public class Product : Item
{
    [XmlElement("Attribute1")]
    public int attribute1;

    public Product() : base() { }

    public Product(Item item) : base(item) { }
}

public class Rune : Product
{
    public bool isMaster = false;

    public Rune() : base() { }

    public Rune(Product product) : base(product)
    {
        if (this.name.Substring(this.name.Length - 2) == "MC")
            isMaster = true;
    }
}