using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InventoryButton : MonoBehaviour {
    public Text countText;
    public Item item;
    public Action<Item> ClickFunction;
    public Inventory currentInventory;

    void Awake()
    {
        countText = this.transform.FindChild("CountText").GetComponent<Text>();
        currentInventory = PlayerInventory.inventory;
    }

    void Update()
    {
        if (currentInventory.GetItemCount(this.item.name) == int.MaxValue)
            this.countText.text = "∞";
        else
            this.countText.text = "x" + currentInventory.GetItemCount(this.item.name).ToString();
    }

    public void Initialize(Item item)
    {
        this.item = item;
        this.GetComponent<Image>().sprite = item.icon;
    }

    public void OnClick()
    {
        if (ClickFunction != null)
            ClickFunction(item);
        else
            Debug.LogError("No Click Functionality defined for InventoryButton [" + item.name + "]");
    }

    public void OnHover()
    {
        //Debug.Log("Ohhh");
    }
}
