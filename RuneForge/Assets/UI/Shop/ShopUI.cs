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
    Button previousPageButton, nextPageButton;
    List<GameObject> pageList = new List<GameObject>();
    Dictionary<string, Sprite> itemImages = new Dictionary<string, Sprite>();

    Tab currentItemsTab = Tab.Materials;
    //Tab currentActionTab = Tab.YourStock;
    Inventory currentInventory;

    int currentPage = 0;
    int buttonsPerPage = 12;
    //int numOfRanks = 4;
    float topButtonPos = 150;

    void Awake()
    {
        actionPanel = this.transform.FindChild("ActionPanel").gameObject;
        itemsPanel = this.transform.FindChild("ItemsPanel").gameObject;
        previousPageButton = GameObject.Find("PreviousPageButton").GetComponent<Button>();
        nextPageButton = GameObject.Find("NextPageButton").GetComponent<Button>();

        //numOfRanks = Rune.runeRanks.Count;
        currentInventory = PlayerInventory.inventory;
        
        /// Might move this to Item/Rune classes to load images into the class rather than here.
        foreach (Rune rune in ItemCollection.runeList)
        {
            itemImages[rune.name] = Resources.Load<Sprite>("ItemSprites/" + rune.name + "Rune");
        }
        foreach (Item material in ItemCollection.materialList)
        {
            itemImages[material.name] = Resources.Load<Sprite>("ItemSprites/" + material.name + "Material");
        }
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
                nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < ItemCollection.materialList.Count);
                break;
            case Tab.Runes:
                nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < ItemCollection.runeList.Count);
                break;
        }
    }

    void DisplayMaterialsPage(int page)
    {        
        ClearPage();

        float yPos = topButtonPos;
        for (int i = 0; i < buttonsPerPage; i++)
        {
            if ((page * buttonsPerPage) + i >= ItemCollection.materialList.Count)
                return;

            GameObject newMaterialButton = CreateItemButton(MaterialButton, yPos);
            yPos -= 30;

            string materialID = ItemCollection.materialList[(page * buttonsPerPage) + i].name;
            newMaterialButton.transform.FindChild("Name").GetComponent<Text>().text = materialID;
            newMaterialButton.transform.FindChild("Count").GetComponent<Text>().text = "x" + currentInventory.GetItemCount(materialID).ToString();

            pageList.Add(newMaterialButton);
        }
        //foreach (KeyValuePair<string, int> kvp in currentInventory.inventoryDict)
        //{
        //    Debug.Log(kvp);
        //}
    }

    void DisplayRunePage(int page)
    {
        ClearPage();

        float yPos = topButtonPos;
        for (int i = 0; i < buttonsPerPage; i++)
        {
            if ((page * buttonsPerPage) + i >= ItemCollection.runeList.Count)
                return;

            GameObject newRuneButton = CreateItemButton(RuneButton, yPos);
            yPos -= 30;

            string runeID = ItemCollection.runeList[(page * buttonsPerPage) + i].name;
            newRuneButton.transform.FindChild("Name").GetComponent<Text>().text = runeID;
            newRuneButton.transform.FindChild("Count").GetComponent<Text>().text = "x" + currentInventory.GetItemCount(runeID).ToString();

            pageList.Add(newRuneButton);
        }
    }

    GameObject CreateItemButton(GameObject buttonType, float yPos)
    {
        GameObject newItemButton = Instantiate(buttonType, itemsPanel.transform.position, Quaternion.identity) as GameObject;
        newItemButton.transform.SetParent(itemsPanel.transform);
        newItemButton.transform.localScale = Vector3.one;
        newItemButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);

        return newItemButton;
    }

    //void DisplayRune(int runeIndex)
    //{
    //    if (runeIndex >= ItemCollection.runeList.Count)
    //    {
    //        Debug.Log("You done gone and out of index error-ed");
    //        return;
    //    }
    //    string runeType = ItemCollection.runeList[runeIndex].name;
    //    int yPos = 150 - (runeIndex % 3 * 120);
    //    foreach (char rank in Rune.runeRanks.Keys)
    //    {
    //        GameObject newRuneButton = Instantiate(RuneButton, itemsPanel.transform.position, Quaternion.identity) as GameObject;
    //        newRuneButton.transform.SetParent(itemsPanel.transform);
    //        newRuneButton.transform.localScale = Vector3.one;
    //        newRuneButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);
    //        yPos -= 30;
    //        string rune = runeType + rank;
    //        newRuneButton.transform.FindChild("Name").GetComponent<Text>().text = runeType;
    //        //newRuneButton.transform.FindChild("Count").GetComponent<Text>().text = "x" + Inventory.GetItemCount(rune).ToString();
    //        newRuneButton.transform.FindChild("Icon").GetComponent<Image>().sprite = itemImages[runeType];
    //        pageList.Add(newRuneButton);
    //    }
    //}

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
        currentItemsTab = Tab.Materials;
        currentPage = 0;
        DisplayTab();
    }

    public void ClickRunesTab()
    {
        currentItemsTab = Tab.Runes;
        currentPage = 0;
        DisplayTab();
    }

    public void ClickYourStock()
    {
        //currentActionTab = Tab.YourStock;
        currentInventory = PlayerInventory.inventory;
    }

    public void ClickShopStock()
    {
        //currentActionTab = Tab.ShopStock;
        currentInventory = ShopInventory.inventory;
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
