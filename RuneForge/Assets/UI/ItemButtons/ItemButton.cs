﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ItemButton : MonoBehaviour
{
    public Item item;

    public bool showHover = true;
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
}