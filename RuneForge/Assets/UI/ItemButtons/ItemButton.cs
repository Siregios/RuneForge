using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ItemButton : MonoBehaviour
{
    public Item item;

    public bool showHover = true;
    public Image attribute1;
    public Image attribute2;
    public Text countText;
    public Inventory.InventoryType inventoryType;
    Inventory referenceInventory;

    public int ItemCount
    {
        get
        {
            return (this.item != null) ? referenceInventory.GetItemCount(this.item.name) : 0;
        }
    }

    void Awake()
    {
        switch (inventoryType)
        {
            case Inventory.InventoryType.PLAYER:
                referenceInventory = PlayerInventory.inventory;
                break;
            case Inventory.InventoryType.SHOP:
                referenceInventory = ShopInventory.inventory;
                break;
        }
    }

    void Update()
    {
        if (countText != null)
        {
            if (ItemCount == int.MaxValue)
                this.countText.text = "∞";
            else
                this.countText.text = "x" + ItemCount.ToString();
        }
    }

    public void Initialize(Item item, List<Action<Item>> buttonFunctions)
    {
        this.item = item;
        this.transform.FindChild("Icon").GetComponent<Image>().sprite = item.icon;
        foreach (Action<Item> function in buttonFunctions)
        {
            this.GetComponent<Button>().onClick.AddListener(() => function(this.item));
        }
        setAttributes();
    }

    public void OnHover(bool active)
    {
        if (showHover)
        {
            if (active)
            {
                HoverInfo.Load();
                HoverInfo.instance.DisplayItem(this);
            }
            else
            {
                HoverInfo.instance.Hide();
            }
        }
    }

    void setAttributes()
    {
        KeyValuePair<string, int> mainItemAttribute = new KeyValuePair<string, int>("", 0);
        KeyValuePair<string, int> secondaryItemAttribute = new KeyValuePair<string, int>("", 0);
        Dictionary<string, string> attributeToColor = new Dictionary<string, string>()
        {
            { "Fire", "red" },
            { "Water", "blue" },
            { "Earth", "yellow" },
            { "Air", "green" }
        };

        //Should only be two things in providedAttributes
        foreach (var kvp in item.providedAttributes)
        {
            if (kvp.Value > mainItemAttribute.Value)
            {
                secondaryItemAttribute = mainItemAttribute;
                mainItemAttribute = kvp;
            }
            else
                secondaryItemAttribute = kvp;
        }

        if (attribute1 != null && mainItemAttribute.Key != "")
        {
            attribute1.sprite = Resources.Load<Sprite>(string.Format("ItemSprites/Charms/{0}_circle", attributeToColor[mainItemAttribute.Key]));
            attribute1.transform.Find("Text").GetComponent<Text>().text = mainItemAttribute.Value.ToString();
        }
        if (attribute2 != null && secondaryItemAttribute.Key != "")
        {
            attribute2.sprite = Resources.Load<Sprite>(string.Format("ItemSprites/Charms/{0}_circle", attributeToColor[secondaryItemAttribute.Key]));
            attribute2.transform.Find("Text").GetComponent<Text>().text = secondaryItemAttribute.Value.ToString();
        }
    }
}