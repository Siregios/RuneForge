using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class IngredientListUI : MonoBehaviour
{
    public GameObject inventoryButton;

    Button previousPageButton, nextPageButton;
    RectTransform buttonArea;
    List<GameObject> pageList = new List<GameObject>();

    Tab currentItemsTab = Tab.Products;

    int currentPage = 0;
    int rows = 5;
    int columns = 5;
    int buttonsPerPage = 36;
    float buttonWidth = 30, buttonHeight = 30, padX = 10, padY = 20;

    void Awake()
    {
        //Debug.Log(ItemCollection.itemList[0].providedAttributes["Water"]);
        previousPageButton = GameObject.Find("PreviousPageButton").GetComponent<Button>();
        nextPageButton = GameObject.Find("NextPageButton").GetComponent<Button>();
        buttonArea = this.transform.FindChild("ButtonArea").GetComponent<RectTransform>();
        buttonWidth = inventoryButton.GetComponent<RectTransform>().rect.width;
        buttonHeight = inventoryButton.GetComponent<RectTransform>().rect.height;
        columns = Mathf.FloorToInt((buttonArea.rect.width + padX) / (buttonWidth + padX));
        rows = Mathf.FloorToInt((buttonArea.rect.height + padY) / (buttonHeight + padY));
        buttonsPerPage = rows * columns;
    }

    void Start()
    {
        ClickProductsTab();
    }

    void Update()
    {
        previousPageButton.interactable = (currentPage > 0);
        nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < pageList.Count);
    }

    public void ClickPreviousPage()
    {
        currentPage--;
        RefreshPage();
    }

    public void ClickNextPage()
    {
        currentPage++;
        RefreshPage();
    }

    public void ClickIngredientsTab()
    {
        currentItemsTab = Tab.Ingredients;
        currentPage = 0;
        RefreshPage();
    }

    public void ClickProductsTab()
    {
        currentItemsTab = Tab.Products;
        currentPage = 0;
        RefreshPage();
    }

    void DisplayPage(int page, string filter)
    {
        ClearPage();

        List<Item> filteredItems = ItemCollection.FilterItem(filter);

        for (int i = 0; i < buttonsPerPage; i++)
        {
            if ((page * buttonsPerPage) + i >= filteredItems.Count)
                return;

            string itemID = filteredItems[(page * buttonsPerPage) + i].name;

            float xPos = (i % columns) * (buttonWidth + padX);
            float yPos = -(i / rows) * (buttonHeight + padY);

            GameObject newInventoryButton = CreateItemButton(inventoryButton, xPos, yPos);
            newInventoryButton.GetComponent<InventoryButton>().Initialize(ItemCollection.itemDict[itemID], PlayerInventory.inventory, ClickButton);

            pageList.Add(newInventoryButton);
        }
    }

    void ClickButton(Item item)
    {
        Debug.Log(item.name);
    }

    //void DisplayMaterialsPage(int page)
    //{
    //    ClearPage();

    //    float yPos = topButtonPos;
    //    for (int i = 0; i < buttonsPerPage; i++)
    //    {
    //        if ((page * buttonsPerPage) + i >= ItemCollection.materialList.Count)
    //            return;

    //        GameObject newMaterialButton = CreateItemButton(MaterialButton, yPos);
    //        yPos -= buttonHeight;

    //        string materialID = ItemCollection.materialList[(page * buttonsPerPage) + i].name;
    //        newMaterialButton.GetComponent<ItemButton>().Initialize(ItemCollection.itemDict[materialID], currentInventory);

    //        pageList.Add(newMaterialButton);
    //    }
    //}

    //void DisplayRunePage(int page)
    //{
    //    ClearPage();

    //    float yPos = topButtonPos;
    //    for (int i = 0; i < buttonsPerPage; i++)
    //    {
    //        if ((page * buttonsPerPage) + i >= ItemCollection.runeList.Count)
    //            return;

    //        GameObject newRuneButton = CreateItemButton(RuneButton, yPos);
    //        yPos -= buttonHeight;

    //        string runeID = ItemCollection.runeList[(page * buttonsPerPage) + i].name;
    //        newRuneButton.GetComponent<ItemButton>().Initialize(ItemCollection.itemDict[runeID], currentInventory);

    //        pageList.Add(newRuneButton);
    //    }
    //}

    GameObject CreateItemButton(GameObject buttonType, float xPos, float yPos)
    {
        GameObject newItemButton = Instantiate(buttonType, this.transform.position, Quaternion.identity) as GameObject;
        newItemButton.transform.SetParent(buttonArea.transform);
        newItemButton.transform.localScale = Vector3.one;
        newItemButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPos, 0);

        return newItemButton;
    }

    void ClearPage()
    {
        foreach (GameObject button in pageList)
        {
            Destroy(button);
        }

        pageList.Clear();
    }

    void RefreshPage()
    {
        switch (currentItemsTab)
        {
            case Tab.Ingredients:
                DisplayPage(currentPage, "ingredient");
                break;
            case Tab.Products:
                DisplayPage(currentPage, "product");
                break;
        }
    }
}