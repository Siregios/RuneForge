using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour {
    private InventoryUIManager inventoryManager;
    private Item item;

    public Text itemName;
    public Image itemIcon;
    public AttrBarGroup attributeBars;

    public void SetItem(Item item)
    {
        this.item = item;
        itemName.text = item.name;
        itemIcon.color = Color.white;
        itemIcon.sprite = item.icon;

        SetAttributeBars(item);
    }

    public void Clear()
    {
        itemName.text = "";
        itemIcon.color = Color.clear;
    }

    void SetAttributeBars(Item item)
    {
        foreach (var kvp in item.providedAttributes)
        {
            attributeBars.SetBar(kvp.Key, kvp.Value);
            attributeBars.SetText(kvp.Key, kvp.Value.ToString());
        }
    }
}
