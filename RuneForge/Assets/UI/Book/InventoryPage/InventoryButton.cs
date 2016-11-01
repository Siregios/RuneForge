using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InventoryButton : MonoBehaviour {
    public Text countText;
    public Item item;
    public Action<Item> ClickFunction;
    public Inventory currentInventory;
    public int ItemCount
    {
        get
        {
            return (this.item != null) ? currentInventory.GetItemCount(this.item.name): 0;
        }
    }

    void Awake()
    {
        countText = this.transform.FindChild("CountText").GetComponent<Text>();
        currentInventory = PlayerInventory.inventory;
    }

    void Update()
    {
        if (ItemCount == int.MaxValue)
            this.countText.text = "∞";
        else
            this.countText.text = "x" + ItemCount.ToString();
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
            Debug.LogErrorFormat("No Click Functionality defined for InventoryButton [{0}]", item.name);
    }

    public void OnHover()
    {
        //Debug.Log("Ohhh");
    }
}
