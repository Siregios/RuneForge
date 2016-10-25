using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InventoryButton : MonoBehaviour {
    public Text countText;
    public Item item;
    public Action<Item> ClickFunction;

    void Awake()
    {
        countText = this.transform.FindChild("CountText").GetComponent<Text>();
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
