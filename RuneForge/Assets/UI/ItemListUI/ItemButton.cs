using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class ItemButton : MonoBehaviour
{
    public Item item;

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

    public virtual void Awake()
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

    public virtual void Update()
    {
        if (countText != null)
        {
            if (ItemCount == int.MaxValue)
                this.countText.text = "∞";
            else
                this.countText.text = "x" + ItemCount.ToString();
        }
    }

    public virtual void Initialize(Item item, List<Action<Item>> buttonFunctions)
    {
        this.item = item;
        this.transform.FindChild("Icon").GetComponent<Image>().sprite = item.icon;
        foreach (Action<Item> function in buttonFunctions)
        {
            this.GetComponent<Button>().onClick.AddListener(() => function(this.item));
        }
    }
}