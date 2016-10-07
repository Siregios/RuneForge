using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemButton : MonoBehaviour {
    GameObject actionPanel;

    void Awake()
    {
        actionPanel = GameObject.Find("ActionPanel");
    }

    public void OnClick()
    {
        string itemName = this.transform.FindChild("Name").GetComponent<Text>().text;
        Sprite icon = this.transform.FindChild("Icon").GetComponent<Image>().sprite;

        actionPanel.transform.FindChild("Name").GetComponent<Text>().text = itemName;
        actionPanel.transform.FindChild("Icon").GetComponent<Image>().sprite = icon;
        actionPanel.transform.FindChild("Price").GetComponent<Text>().text = ItemCollection.itemDict[itemName].price.ToString();
    }
}
