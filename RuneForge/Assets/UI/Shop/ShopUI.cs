using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Tab
{
    Materials,
    Runes
}

public class ShopUI : MonoBehaviour {

    public GameObject RuneButton, MaterialButton;
    GameObject actionPanel, itemsPanel;
    Button materialsTabButton, runesTabButton, previousPageButton, nextPageButton;
    List<GameObject> pageList = new List<GameObject>();
    Tab currentTab;

    int currentPage = 0;
    int buttonsPerPage = 12;
    int numOfRanks = Inventory.runeRanks.Length;

    void Awake()
    {
        actionPanel = this.transform.FindChild("ActionPanel").gameObject;
        itemsPanel = this.transform.FindChild("ItemsPanel").gameObject;
        materialsTabButton = GameObject.Find("MaterialsTabButton").GetComponent<Button>();
        runesTabButton = GameObject.Find("RunesTabButton").GetComponent<Button>();
        previousPageButton = GameObject.Find("PreviousPageButton").GetComponent<Button>();
        nextPageButton = GameObject.Find("NextPageButton").GetComponent<Button>();
    }

    void Start()
    {
        ClickMaterialsTab();
    }

    void Update()
    {
        previousPageButton.interactable = (currentPage > 0);
        switch (currentTab)
        {
            case Tab.Materials:
                nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage < Inventory.materialList.Count);
                break;
            case Tab.Runes:
                nextPageButton.interactable = ((currentPage + 1) * buttonsPerPage / numOfRanks < Inventory.runeList.Count);
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
        currentTab = Tab.Materials;

        currentPage = 0;
        DisplayTab();
    }

    public void ClickRunesTab()
    {
        runesTabButton.interactable = false;
        materialsTabButton.interactable = true;
        currentTab = Tab.Runes;

        currentPage = 0;
        DisplayTab();
    }

    void DisplayMaterialsPage(int page)
    {        
        ClearPage();

        int yPos = 150;
        for (int i = 0; i < 12; i++)
        {
            if ((page * 12) + i >= Inventory.materialList.Count)
                return;

            GameObject newMaterialButton = Instantiate(MaterialButton, itemsPanel.transform.position, Quaternion.identity) as GameObject;
            newMaterialButton.transform.SetParent(itemsPanel.transform);
            newMaterialButton.transform.localScale = Vector3.one;
            newMaterialButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);
            yPos -= 30;

            string material = Inventory.materialList[(page * 12) + i];
            newMaterialButton.transform.FindChild("Text").GetComponent<Text>().text = material;
            newMaterialButton.transform.FindChild("Count").GetComponent<Text>().text = "x" + Inventory.GetItemCount(material).ToString();

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
        if (runeIndex >= Inventory.runeList.Count)
        {
            Debug.Log("You done gone and out of index error-ed");
            return;
        }

        string runeType = Inventory.runeList[runeIndex];
        int yPos = 150 - (runeIndex % 3 * 120);
        foreach (char rank in Inventory.runeRanks)
        {
            GameObject newRuneButton = Instantiate(RuneButton, itemsPanel.transform.position, Quaternion.identity) as GameObject;
            newRuneButton.transform.SetParent(itemsPanel.transform);
            newRuneButton.transform.localScale = Vector3.one;
            newRuneButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, yPos, 0);
            yPos -= 30;

            string rune = runeType + rank;
            newRuneButton.transform.FindChild("Text").GetComponent<Text>().text = runeType;
            newRuneButton.transform.FindChild("Count").GetComponent<Text>().text = "x" + Inventory.GetItemCount(rune).ToString();

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
        switch (currentTab)
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
