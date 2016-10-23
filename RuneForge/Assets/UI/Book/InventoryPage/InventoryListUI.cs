using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class InventoryListUI : MonoBehaviour
{
    public GameObject inventoryButton;
    public GameObject pairedPanel;
    public Action<Item> inventoryButtonFunction;

    InputField searchInput;
    Button previousPageButton, nextPageButton;
    RectTransform buttonArea;

    Dictionary<string, FilterInventoryButton> filterToggles = new Dictionary<string, FilterInventoryButton>();
    List<GameObject> pageList = new List<GameObject>();

    int currentPage = 0;
    string filterString = "";

    Inventory currentInventory;

    float buttonWidth = 30, buttonHeight = 30, padX = 10, padY = 20;
    int rows = 5;
    int columns = 5;
    int buttonsPerPage = 25;

    void Awake()
    {
        if (pairedPanel.GetComponent<TransactionUI>() != null)
            inventoryButtonFunction = pairedPanel.GetComponent<TransactionUI>().LoadItem;
        else if (pairedPanel.GetComponent<RecipeUI>() != null)
            ;
        else
            Debug.LogError("Paired Panel missing appropriate script");

        searchInput = this.transform.FindChild("SearchInput").GetComponent<InputField>();
        previousPageButton = this.transform.FindChild("PreviousPageButton").GetComponent<Button>();
        nextPageButton = this.transform.FindChild("NextPageButton").GetComponent<Button>();
        buttonArea = this.transform.FindChild("ButtonArea").GetComponent<RectTransform>();

        currentInventory = PlayerInventory.inventory;

        buttonWidth = inventoryButton.GetComponent<RectTransform>().rect.width;
        buttonHeight = inventoryButton.GetComponent<RectTransform>().rect.height;
        columns = Mathf.FloorToInt((buttonArea.rect.width + padX) / (buttonWidth + padX));
        rows = Mathf.FloorToInt((buttonArea.rect.height + padY) / (buttonHeight + padY));
        buttonsPerPage = rows * columns;
    }

    void Start()
    {
        DisplayNewFilter();
    }

    void Update()
    {
        previousPageButton.interactable = (currentPage > 0);
        nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < pageList.Count);
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
            newInventoryButton.GetComponent<InventoryButton>().Initialize(ItemCollection.itemDict[itemID], this.currentInventory, inventoryButtonFunction);

            pageList.Add(newInventoryButton);
        }
    }

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

    public void ChangeInventory(Inventory newInv)
    {
        this.currentInventory = newInv;
        DisplayNewFilter();
    }

    public void ClickPreviousPage()
    {
        currentPage--;
        DisplayPage(currentPage, filterString);
    }

    public void ClickNextPage()
    {
        currentPage++;
        DisplayPage(currentPage, filterString);
    }

    public void ClickFilterButton(FilterInventoryButton filterButton)
    {
        filterToggles[filterButton.filterString] = filterButton;

        if (filterButton.isPressed)
        {
            this.filterString = filterButton.filterString;

            foreach (KeyValuePair<string, FilterInventoryButton> kvp in filterToggles)
            {
                if (kvp.Key != filterString)
                {
                    kvp.Value.Release();
                }
            }
        }
        else
        {
            this.filterString = "ALL";
        }

        searchInput.text = this.filterString;

        DisplayNewFilter();
    }

    public void OnSearchSubmit()
    {
        this.filterString = searchInput.text;
        DisplayNewFilter();
    }

    void DisplayNewFilter()
    {
        currentPage = 0;
        DisplayPage(currentPage, filterString);
    }
}