using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script is trash - don't use it
/// </summary>
//public class ItemListUI : MonoBehaviour {
//    public GameObject RuneButton, MaterialButton;
//    //public GameObject ItemButton;

//    Button previousPageButton, nextPageButton;
//    List<GameObject> pageList = new List<GameObject>();

//    Inventory currentInventory;
//    Tab currentItemsTab = Tab.Materials;

//    int currentPage = 0;
//    int buttonsPerPage = 12;
//    float buttonHeight = 30;
//    float topButtonPos = 150;

//    void Awake()
//    {
//        previousPageButton = GameObject.Find("PreviousPageButton").GetComponent<Button>();
//        nextPageButton = GameObject.Find("NextPageButton").GetComponent<Button>();
//        currentInventory = PlayerInventory.inventory;
//    }

//    void Start()
//    {
//        ClickMaterialsTab();
//    }

//    void Update()
//    {
//        previousPageButton.interactable = (currentPage > 0);
//        switch (currentItemsTab)
//        {
//            case Tab.Materials:
//                nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < ItemCollection.materialList.Count);
//                break;
//            case Tab.Runes:
//                nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < ItemCollection.runeList.Count);
//                break;
//        }
//    }

//    public void ChangeInventory(Inventory inventory)
//    {
//        currentInventory = inventory;
//        RefreshPage();
//    }

//    public void ClickPreviousPage()
//    {
//        currentPage--;
//        RefreshPage();
//    }

//    public void ClickNextPage()
//    {
//        currentPage++;
//        RefreshPage();
//    }

//    public void ClickMaterialsTab()
//    {
//        currentItemsTab = Tab.Materials;
//        currentPage = 0;
//        RefreshPage();
//    }

//    public void ClickRunesTab()
//    {
//        currentItemsTab = Tab.Runes;
//        currentPage = 0;
//        RefreshPage();
//    }

//    void DisplayMaterialsPage(int page)
//    {
//        ClearPage();

//        float yPos = topButtonPos;
//        for (int i = 0; i < buttonsPerPage; i++)
//        {
//            if ((page * buttonsPerPage) + i >= ItemCollection.materialList.Count)
//                return;

//            GameObject newMaterialButton = CreateItemButton(MaterialButton, yPos);
//            yPos -= buttonHeight;

//            string materialID = ItemCollection.materialList[(page * buttonsPerPage) + i].name;
//            newMaterialButton.GetComponent<InventoryButton>().Initialize(ItemCollection.itemDict[materialID], currentInventory, ClickButton);

//            pageList.Add(newMaterialButton);
//        }
//    }

//    void DisplayRunePage(int page)
//    {
//        ClearPage();

//        float yPos = topButtonPos;
//        for (int i = 0; i < buttonsPerPage; i++)
//        {
//            if ((page * buttonsPerPage) + i >= ItemCollection.runeList.Count)
//                return;

//            GameObject newRuneButton = CreateItemButton(RuneButton, yPos);
//            yPos -= buttonHeight;

//            string runeID = ItemCollection.runeList[(page * buttonsPerPage) + i].name;
//            newRuneButton.GetComponent<InventoryButton>().Initialize(ItemCollection.itemDict[runeID], currentInventory, ClickButton);

//            pageList.Add(newRuneButton);
//        }
//    }

//    void ClickButton(Item item)
//    {
//        Debug.Log(item.name);
//    }

//    GameObject CreateItemButton(GameObject buttonType, float yPos)
//    {
//        GameObject newItemButton = Instantiate(buttonType, this.transform.position, Quaternion.identity) as GameObject;
//        newItemButton.transform.SetParent(this.transform);
//        newItemButton.transform.localScale = Vector3.one;
//        newItemButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);

//        return newItemButton;
//    }

//    void ClearPage()
//    {
//        foreach (GameObject button in pageList)
//        {
//            Destroy(button);
//        }

//        pageList.Clear();
//    }

//    void RefreshPage()
//    {
//        switch (currentItemsTab)
//        {
//            case Tab.Materials:
//                DisplayMaterialsPage(currentPage);
//                break;
//            case Tab.Runes:
//                DisplayRunePage(currentPage);
//                break;
//        }
//    }
//}
