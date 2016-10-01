using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopUI : MonoBehaviour {

    public GameObject RuneButton;
    GameObject actionPanel;
    GameObject itemsPanel;

    void Awake()
    {
        actionPanel = this.transform.FindChild("ActionPanel").gameObject;
        itemsPanel = this.transform.FindChild("ItemsPanel").gameObject;
    }

    public void DisplayRunePage(int page)
    {
        DisplayRune(page);
        DisplayRune(page + 1);
        DisplayRune(page + 2);
    }

    void DisplayRune(int runeIndex)
    {
        string runeType = Inventory.runeList[runeIndex];
        int yPos = 150 - (runeIndex % 3 * 120);
        foreach (char rank in "SABC")
        {
            GameObject newRuneButton = Instantiate(RuneButton, itemsPanel.transform.position, Quaternion.identity) as GameObject;
            newRuneButton.transform.SetParent(itemsPanel.transform);
            newRuneButton.transform.localScale = Vector3.one;
            newRuneButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);
            yPos -= 30;

            string rune = runeType + rank;
            newRuneButton.transform.FindChild("Text").GetComponent<Text>().text = runeType;
            newRuneButton.transform.FindChild("Count").GetComponent<Text>().text = "x" + Inventory.GetItemCount(rune).ToString();
        }
    }

    public void DisplayMaterialsPage(int page)
    {
        DisplayRune(page);
        DisplayRune(page + 1);
        DisplayRune(page + 2);
    }
}
