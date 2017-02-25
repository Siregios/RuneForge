﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ItemListUI : MonoBehaviour
{
    public GameObject itemButton;
    public string defaultFilter = "ALL";
    public bool displayZeroCountItems = true;
    public Inventory.InventoryType inventoryType;
    Inventory referenceInventory;
    
    public float padX = 10, padY = 20;
    List<Item> defaultList = new List<Item>();
    List<Item> itemList = new List<Item>();
    List<Action<Item>> buttonClickFunctions = new List<Action<Item>>();

    InputField searchInput;
    Button previousPageButton, nextPageButton;
    RectTransform buttonArea;

    Dictionary<string, FilterInventoryButton> filterToggles = new Dictionary<string, FilterInventoryButton>();
    List<ItemButton> buttonList = new List<ItemButton>();

    int currentPage = 0;
    string filterString = "";

    float buttonWidth = 30, buttonHeight = 30;
    int rows = 5;
    int columns = 5;
    int buttonsPerPage = 25;

    void Awake()
    {
        switch (inventoryType)
        {
            case Inventory.InventoryType.PLAYER:
                referenceInventory = PlayerInventory.inventory;
                break;
            case Inventory.InventoryType.SHOP:
                referenceInventory = ShopInventory.inventory;
                break;
        }
        searchInput = this.transform.FindChild("SearchInput").GetComponent<InputField>();
        previousPageButton = this.transform.FindChild("PreviousPageButton").GetComponent<Button>();
        nextPageButton = this.transform.FindChild("NextPageButton").GetComponent<Button>();
        buttonArea = this.transform.FindChild("ButtonArea").GetComponent<RectTransform>();

        buttonWidth = itemButton.GetComponent<RectTransform>().rect.width;
        buttonHeight = itemButton.GetComponent<RectTransform>().rect.height;
        columns = Mathf.FloorToInt((buttonArea.rect.width + padX) / (buttonWidth + padX));
        rows = Mathf.FloorToInt((buttonArea.rect.height + padY) / (buttonHeight + padY));
        buttonsPerPage = rows * columns;
    }

    void Start()
    {
        defaultList = ItemCollection.FilterItemList(defaultFilter);
        filterString = defaultFilter;
        DisplayNewFilter(filterString);
    }

    void LateUpdate()
    {
        previousPageButton.interactable = (currentPage > 0);
        nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage <= itemList.Count);
        //Debug.Log(buttonList.Count);
    }

    public void AddButtonFunction(Action<Item> clickFunction)
    {
        buttonClickFunctions.Add(clickFunction);
    }

    void DisplayPage(string filter)
    {
        ClearPage();

        for (int i = 0; i < buttonsPerPage; i++)
        {
            if ((currentPage * buttonsPerPage) + i >= itemList.Count)
                return;

            string itemID = itemList[(currentPage * buttonsPerPage) + i].name;

            float xPos = (i % columns) * (buttonWidth + padX);
            float yPos = -(i / columns) * (buttonHeight + padY);

            ItemButton newItemButton = CreateItemButton(ItemCollection.itemDict[itemID], xPos, yPos);

            buttonList.Add(newItemButton);
        }
    }

    ItemButton CreateItemButton(Item item, float xPos, float yPos)
    {
        GameObject newItemButton = Instantiate(itemButton, this.transform.position, Quaternion.identity) as GameObject;
        newItemButton.transform.SetParent(buttonArea.transform);
        newItemButton.transform.localScale = Vector3.one;
        newItemButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPos, 0);

        ItemButton button = newItemButton.GetComponent<ItemButton>();
        button.Initialize(item, referenceInventory, buttonClickFunctions);

        return button;
    }

    void ClearPage()
    {
        foreach (ItemButton button in buttonList)
        {
            Destroy(button.gameObject);
        }

        buttonList.Clear();
    }

    public void RefreshPage()
    {
        DisplayNewFilter(filterString);
        //DisplayPage(filterString);
    }

    public void ClickPreviousPage()
    {
        currentPage--;
        DisplayNewFilter(filterString);
        //DisplayPage(filterString);
    }

    public void ClickNextPage()
    {
        currentPage++;
        DisplayNewFilter(filterString);
        //DisplayPage(filterString);
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
            this.filterString = defaultFilter;
        }
        currentPage = 0;
        DisplayNewFilter(this.filterString);
    }

    public void OnSearchSubmit()
    {
        currentPage = 0;
        DisplayNewFilter(searchInput.text);
    }

    void DisplayNewFilter(string filter)
    {
        this.filterString = filter;

        List<Item> filteredItems = ItemCollection.FilterSpecificList(defaultList, filter);
        itemList.Clear();

        if (!displayZeroCountItems)
        {
            foreach (Item item in filteredItems)
            {
                if (referenceInventory.GetItemCount(item) > 0)
                    itemList.Add(item);
            }
        }
        else
        {
            itemList = filteredItems;
        }

        //currentPage = 0;
        DisplayPage(filterString);
        searchInput.text = this.filterString;
    }
}