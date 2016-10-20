using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemButton : MonoBehaviour {
    BookUI bookUI;
    Item item;
    Inventory itemOwner;

    void Awake()
    {
        bookUI = GameObject.Find("BookPanel").GetComponent<BookUI>();
    }

    void Update()
    {
        if (itemOwner.GetItemCount(item.name) == int.MaxValue)
            this.transform.FindChild("Count").GetComponent<Text>().text = "∞";
        else
            this.transform.FindChild("Count").GetComponent<Text>().text = "x" + itemOwner.GetItemCount(item.name).ToString();
    }

    public void Initialize(Item item, Inventory owner)
    {
        this.item = item;
        this.itemOwner = owner;
        this.transform.FindChild("Name").GetComponent<Text>().text = item.name;
        this.transform.FindChild("Icon").GetComponent<Image>().sprite = item.icon;
    }

    public void OnClick()
    {
        bookUI.SelectItem(this.item);
    }
}
