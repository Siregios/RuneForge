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
    Button previousPageButton, nextPageButton, actionButton;
    Text actionName, actionPrice;
    //Sprite actionIcon;
    List<GameObject> pageList = new List<GameObject>();
    //Dictionary<string, Sprite> itemImages = new Dictionary<string, Sprite>();

    int currentPage = 0;
    int buttonsPerPage = 12;
    float buttonHeight = 30;
    float topButtonPos = 150;

    Tab currentItemsTab = Tab.Materials;
    Tab currentActionTab = Tab.YourStock;
    Inventory currentInventory;
    Item currentItem;

    public Item CurrentItem
    {
        get { return this.currentItem; }
        set { this.currentItem = value; }
    }

    void Awake()
    {
        actionPanel = this.transform.FindChild("ActionPanel").gameObject;
        itemsPanel = this.transform.FindChild("ItemsPanel").gameObject;
        previousPageButton = GameObject.Find("PreviousPageButton").GetComponent<Button>();
        nextPageButton = GameObject.Find("NextPageButton").GetComponent<Button>();

        actionName = actionPanel.transform.FindChild("Name").GetComponent<Text>();
        //actionIcon = actionPanel.transform.FindChild("Icon").GetComponent<Sprite>();
        actionPrice = actionPanel.transform.FindChild("Price").GetComponent<Text>();
        actionButton = actionPanel.transform.FindChild("ActionButton").GetComponent<Button>();

        currentInventory = PlayerInventory.inventory;
        
        /// Might move this to Item/Rune classes to load images into the class rather than here.
        //foreach (Rune rune in ItemCollection.runeList)
        //{
        //    itemImages[rune.name] = Resources.Load<Sprite>("ItemSprites/" + rune.name + "Rune");
        //}
        //foreach (Item material in ItemCollection.materialList)
        //{
        //    itemImages[material.name] = Resources.Load<Sprite>("ItemSprites/" + material.name + "Material");
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
            yPos -= buttonHeight;

            string materialID = ItemCollection.materialList[(page * buttonsPerPage) + i].name;
            newMaterialButton.GetComponent<ItemButton>().Initialize(ItemCollection.itemDict[materialID], currentInventory.GetItemCount(materialID));

            pageList.Add(newMaterialButton);
        }
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
            yPos -= buttonHeight;

            string runeID = ItemCollection.runeList[(page * buttonsPerPage) + i].name;
            newRuneButton.GetComponent<ItemButton>().Initialize(ItemCollection.itemDict[runeID], currentInventory.GetItemCount(runeID));

            pageList.Add(newRuneButton);
        }
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
        currentActionTab = Tab.YourStock;
        currentInventory = PlayerInventory.inventory;
        ClearActionPanel();
        DisplayTab();
    }

    public void ClickShopStock()
    {
        currentActionTab = Tab.ShopStock;
        currentInventory = ShopInventory.inventory;
        ClearActionPanel();
        DisplayTab();
    }

    public void SelectItem(Item item)
    {
        currentItem = item;

        actionName.text = currentItem.name;
        //actionIcon.sprite = currentItem.icon;
        actionPrice.text = ItemCollection.itemDict[currentItem.name].price.ToString();
        actionButton.interactable = true;
    } 

    public void ClickTrade()
    {
        switch (currentActionTab)
        {
            //Selling
            case Tab.YourStock:
                //Edit this for sell-x
                TradeManager.SellItem(currentItem, 1);
                break;

            //Buying
            case Tab.ShopStock:
                TradeManager.BuyItem(currentItem, 1);
                break;
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

    void ClearActionPanel()
    {
        actionName.text = "";
        //actionIcon.sprite = NONE;
        actionPrice.text = "";
        actionButton.interactable = false;
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
