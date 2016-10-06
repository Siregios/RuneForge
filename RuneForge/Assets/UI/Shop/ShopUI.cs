using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Tab
{
    Materials,
    Runes,
    YourStock,
    ShopStock
}

public class ShopUI : MonoBehaviour {

    public GameObject RuneButton, MaterialButton;
    GameObject actionPanel, itemsPanel;
    Button materialsTabButton, runesTabButton, previousPageButton, nextPageButton;
    List<GameObject> pageList = new List<GameObject>();
    Dictionary<string, Sprite> itemImages = new Dictionary<string, Sprite>();

    Tab currentItemsTab;
    Tab currentActionTab;

    int currentPage = 0;
    int buttonsPerPage = 12;
    int numOfRanks = Rune.runeRanks.Length;

    void Awake()
    {
        actionPanel = this.transform.FindChild("ActionPanel").gameObject;
        itemsPanel = this.transform.FindChild("ItemsPanel").gameObject;
        materialsTabButton = GameObject.Find("MaterialsTabButton").GetComponent<Button>();
        runesTabButton = GameObject.Find("RunesTabButton").GetComponent<Button>();
        previousPageButton = GameObject.Find("PreviousPageButton").GetComponent<Button>();
        nextPageButton = GameObject.Find("NextPageButton").GetComponent<Button>();
        //foreach (string name in Inventory.runeList)
        //{
        //    itemImages[name] = Resources.Load<Sprite>("ItemSprites/" + name + "Rune");
        //}
        //foreach (string name in Inventory.materialList)
        //{
        //    itemImages[name] = Resources.Load<Sprite>("ItemSprites/" + name + "Material");
        //}
    }

    void Start()
    {
        ClickMaterialsTab();
    }

    void Update()
    {
        previousPageButton.interactable = (currentPage > 0);
        switch (currentItemsTab)
        {
            case Tab.Materials:
                nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < Item.materialList.Count);
                break;
            case Tab.Runes:
                nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage / numOfRanks < Item.runeList.Count);
                break;
        }
    }

    public void ClickPreviousPage()
    {
        currentPage--;
        DisplayTab();
    }
    
    public void ClickNextPage()
    {
        currentPage++;
        DisplayTab();
    }

    public void ClickMaterialsTab()
    {
        materialsTabButton.interactable = false;
        runesTabButton.interactable = true;
        currentItemsTab = Tab.Materials;

        currentPage = 0;
        DisplayTab();
    }

    public void ClickRunesTab()
    {
        runesTabButton.interactable = false;
        materialsTabButton.interactable = true;
        currentItemsTab = Tab.Runes;

        currentPage = 0;
        DisplayTab();
    }

    void DisplayMaterialsPage(int page)
    {        
        ClearPage();

        int yPos = 150;
        for (int i = 0; i < 12; i++)
        {
            if ((page * 12) + i >= Item.materialList.Count)
                return;

            GameObject newMaterialButton = Instantiate(MaterialButton, itemsPanel.transform.position, Quaternion.identity) as GameObject;
            newMaterialButton.transform.SetParent(itemsPanel.transform);
            newMaterialButton.transform.localScale = Vector3.one;
            newMaterialButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);
            yPos -= 30;

            string material = Item.materialList[(page * 12) + i];
            newMaterialButton.transform.FindChild("Name").GetComponent<Text>().text = material;
            //newMaterialButton.transform.FindChild("Count").GetComponent<Text>().text = "x" + Inventory.GetItemCount(material).ToString();

            pageList.Add(newMaterialButton);
        }
    }

    void DisplayRunePage(int page)
    {
        ClearPage();
        int runeTypesPerPage = buttonsPerPage / numOfRanks;
        page *= runeTypesPerPage;
        for (int i = 0; i < runeTypesPerPage; i++)
        {
            DisplayRune(page + i);
        }
    }

    void DisplayRune(int runeIndex)
    {
        if (runeIndex >= Item.runeList.Count)
        {
            Debug.Log("You done gone and out of index error-ed");
            return;
        }

        string runeType = Item.runeList[runeIndex];
        int yPos = 150 - (runeIndex % 3 * 120);
        foreach (char rank in Rune.runeRanks)
        {
            GameObject newRuneButton = Instantiate(RuneButton, itemsPanel.transform.position, Quaternion.identity) as GameObject;
            newRuneButton.transform.SetParent(itemsPanel.transform);
            newRuneButton.transform.localScale = Vector3.one;
            newRuneButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);
            yPos -= 30;

            string rune = runeType + rank;
            newRuneButton.transform.FindChild("Name").GetComponent<Text>().text = runeType;
            //newRuneButton.transform.FindChild("Count").GetComponent<Text>().text = "x" + Inventory.GetItemCount(rune).ToString();
            newRuneButton.transform.FindChild("Icon").GetComponent<Image>().sprite = itemImages[runeType];

            pageList.Add(newRuneButton);
        }
    }

    void ClearPage()
    {
        foreach (GameObject button in pageList)
        {
            Destroy(button);
        }

        pageList.Clear();
    }

    void DisplayTab()
    {
        switch (currentItemsTab)
        {
            case Tab.Materials:
                DisplayMaterialsPage(currentPage);
                break;
            case Tab.Runes:
                DisplayRunePage(currentPage);
                break;
        }
    }
}
