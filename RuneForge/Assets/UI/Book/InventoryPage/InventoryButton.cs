using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InventoryButton : MonoBehaviour {
    Text countText;
    Item item;
    Inventory itemOwner;
    Action<Item> clickFunction;

    void Awake()
    {
        countText = this.transform.FindChild("CountText").GetComponent<Text>();
    }

    void Update()
    {
        if (itemOwner.GetItemCount(item.name) == int.MaxValue)
        {
            Debug.Log("IsInfinite");
            this.countText.text = "∞";
        }
            
        else
            this.countText.text = "x" + itemOwner.GetItemCount(item.name).ToString();
    }

    public void Initialize(Item item, Inventory owner, Action<Item> clickFunction)
    {
        this.item = item;
        this.itemOwner = owner;
        this.clickFunction = clickFunction;
        this.GetComponent<Image>().sprite = item.icon;
    }

    public void OnClick()
    {
        this.clickFunction(item);
    }

    public void OnHover()
    {
        Debug.Log("Ohhh");
    }
}
