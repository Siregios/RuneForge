using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class IngredientListUI : MonoBehaviour
{
    public GameObject ingredientButton;

    Button previousPageButton, nextPageButton;
    List<GameObject> pageList = new List<GameObject>();

    Tab currentItemsTab = Tab.Products;

    int currentPage = 0;
    int rows = 6;
    int columns = 6;
    int buttonsPerPage = 36;
    float buttonWidth = 30, buttonHeight = 30, pad = 10;
    Vector2 topLeftPos = new Vector2(-100, 100);

    void Awake()
    {
        Debug.Log(ItemCollection.itemList[0].providedAttributes["Water"]);
        previousPageButton = GameObject.Find("PreviousPageButton").GetComponent<Button>();
        nextPageButton = GameObject.Find("NextPageButton").GetComponent<Button>();
        buttonWidth = ingredientButton.GetComponent<RectTransform>().rect.width;
        buttonHeight = ingredientButton.GetComponent<RectTransform>().rect.height;
        buttonsPerPage = rows * columns;
    }

    void Start()
    {
        ClickProductsTab();
    }

    void Update()
    {
        //previousPageButton.interactable = (currentPage > 0);
        //switch (currentItemsTab)
        //{
        //    case Tab.Materials:
        //        nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < ItemCollection.materialList.Count);
        //        break;
        //    case Tab.Runes:
        //        nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < ItemCollection.runeList.Count);
        //        break;
        //}
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

    public void ClickMaterialsTab()
    {
        currentItemsTab = Tab.Materials;
        currentPage = 0;
        RefreshPage();
    }

    public void ClickRunesTab()
    {
        currentItemsTab = Tab.Runes;
        currentPage = 0;
        RefreshPage();
    }

    public void ClickProductsTab()
    {
        currentItemsTab = Tab.Products;
        currentPage = 0;
        RefreshPage();
    }

    void DisplayPage(int page, List<Item> itemList)
    {
        ClearPage();

        float xPos = topLeftPos.x;
        float yPos = topLeftPos.y;

        for (int i = 0; i < buttonsPerPage; i++)
        {
            if ((page * buttonsPerPage) + i >= itemList.Count)
                return;

            if ((i % columns) == 0)
            {
                xPos = topLeftPos.x;
                yPos -= buttonHeight + pad;
            }

            GameObject newIngredientButton = CreateItemButton(ingredientButton, xPos, yPos);
            xPos += buttonWidth + pad;


            pageList.Add(newIngredientButton);
        }
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
        newItemButton.transform.SetParent(this.transform);
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
            case Tab.Materials:
                //DisplayMaterialsPage(currentPage);
                DisplayPage(currentPage, ItemCollection.materialList);
                break;
            case Tab.Runes:
                //DisplayRunePage(currentPage);
                DisplayPage(currentPage, ItemCollection.runeList.Cast<Item>().ToList());
                break;
            case Tab.Products:
                DisplayPage(currentPage, ItemCollection.productList);
                break;
        }
    }
}