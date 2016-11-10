using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class InventoryListUI : MonoBehaviour
{
    public GameObject inventoryButton;
    
    // The left panel must call ModifyAllButtons to pass in how inventoryButtons this script spawns should behave.
    Action<InventoryButton> buttonBehavior;

    InputField searchInput;
    Button previousPageButton, nextPageButton;
    RectTransform buttonArea;

    Dictionary<string, FilterInventoryButton> filterToggles = new Dictionary<string, FilterInventoryButton>();
    List<InventoryButton> buttonList = new List<InventoryButton>();

    int currentPage = 0;
    string filterString = "";

    float buttonWidth = 30, buttonHeight = 30, padX = 10, padY = 20;
    int rows = 5;
    int columns = 5;
    int buttonsPerPage = 25;

    void Awake()
    {
        searchInput = this.transform.FindChild("SearchInput").GetComponent<InputField>();
        previousPageButton = this.transform.FindChild("PreviousPageButton").GetComponent<Button>();
        nextPageButton = this.transform.FindChild("NextPageButton").GetComponent<Button>();
        buttonArea = this.transform.FindChild("ButtonArea").GetComponent<RectTransform>();

        buttonWidth = inventoryButton.GetComponent<RectTransform>().rect.width;
        buttonHeight = inventoryButton.GetComponent<RectTransform>().rect.height;
        columns = Mathf.FloorToInt((buttonArea.rect.width + padX) / (buttonWidth + padX));
        rows = Mathf.FloorToInt((buttonArea.rect.height + padY) / (buttonHeight + padY));
        buttonsPerPage = rows * columns;
    }

    void Start()
    {
        DisplayNewFilter(filterString);
    }

    void OnEnable()
    {
        MasterGameManager.instance.uiManager.uiOpen = true;
    }

    void OnDisable()
    {
        MasterGameManager.instance.uiManager.uiOpen = false;
    }

    void Update()
    {
        previousPageButton.interactable = (currentPage > 0);
        nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < buttonList.Count);
    }

    // Sets the class variable for how a button should behave and refreshes all currently displayed Inventory Buttons to act with the new behavior.
    public void ModifyAllButtons(Action<InventoryButton> newBehavior)
    {
        this.buttonBehavior = newBehavior;
        foreach (InventoryButton button in buttonList)
        {
            ModifyButton(button);
        }
    }

    void ModifyButton(InventoryButton button)
    {
        if (buttonBehavior != null)
            buttonBehavior(button);
        else
            Debug.LogWarning("InventoryListUI cannot modify inventory buttons - No ButtonBehavior has been defined by the left page");
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
            InventoryButton button = newInventoryButton.GetComponent<InventoryButton>();

            button.Initialize(ItemCollection.itemDict[itemID]);
            ModifyButton(button);

            buttonList.Add(button);
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
        foreach (InventoryButton button in buttonList)
        {
            Destroy(button.gameObject);
        }

        buttonList.Clear();
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

        DisplayNewFilter(this.filterString);
        
    }

    public void OnSearchSubmit()
    {
        //this.filterString = searchInput.text;
        DisplayNewFilter(searchInput.text);
    }

    public void DisplayNewFilter(string filter)
    {
        this.filterString = filter;
        currentPage = 0;
        DisplayPage(currentPage, filterString);
        searchInput.text = this.filterString;
    }
}