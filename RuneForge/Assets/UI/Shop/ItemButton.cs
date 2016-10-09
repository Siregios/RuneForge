using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemButton : MonoBehaviour {
    ShopUI shopUI;
    Item item;

    void Awake()
    {
        shopUI = GameObject.Find("ShopPanel").GetComponent<ShopUI>();
    }

    public void Initialize(Item item, int count)
    {
        this.item = item;
        this.transform.FindChild("Name").GetComponent<Text>().text = item.name;
        if (count == int.MaxValue)
            this.transform.FindChild("Count").GetComponent<Text>().text = "∞";
        else
            this.transform.FindChild("Count").GetComponent<Text>().text = "x" + count.ToString();
    }

    public void OnClick()
    {
        shopUI.SelectItem(this.item);
    }
}
