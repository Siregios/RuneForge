﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecipeUI : MonoBehaviour {
    Item productItem;
    private AudioManager AudioManager;


    public Button cancelButton, pinSelectButton, pinRandomButton;
    public ItemListUI productItemList, ingredientItemList;
    Text productName;
    Image productIcon;
    Text recipeText;

    //Refactor this later?
    //Roldan: Yes
    //Peter & Efren: Same
    //Edwin: FUCK
    public List<IngredientEntry> ingredientEntryList;
    Dictionary<string, int> addedIngredients = new Dictionary<string, int>();

    void Awake()
    {
        productItemList.AddButtonFunction(AddProduct);
        ingredientItemList.AddButtonFunction(AddIngredient);

        productName = this.transform.Find("ProductName").GetComponent<Text>();
        productIcon = this.transform.Find("ProductIconPanel/ProductIcon").GetComponent<Image>();
        recipeText = this.transform.Find("Recipe").GetComponent<Text>();

        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        //invListUI.ModifyAllButtons(ProductButtonBehavior);
    }

    void OnEnable()
    {
        MasterGameManager.instance.uiManager.uiOpen = true;
        AudioManager.PlaySound(7);

    }

    void OnDisable()
    {
        MasterGameManager.instance.uiManager.uiOpen = false;
        AudioManager.PlaySound(8);
    }

    void Start()
    {
        ImportProductMode();
    }
    
    void Update()
    {
        if (isRecipeMet() && !MasterGameManager.instance.workOrderManager.IsFull())
        {
            pinSelectButton.interactable = true;
            pinRandomButton.interactable = true;
        }
        else
        {
            pinSelectButton.interactable = false;
            pinRandomButton.interactable = false;
        }
    }

    public void ImportProductMode()
    {
        ingredientItemList.gameObject.SetActive(false);
        
        productItemList.gameObject.SetActive(true);
        productItemList.DisplayNewFilter("product");
        productItem = null;
        productName.text = "";
        productIcon.color = Color.clear;
        recipeText.text = "";
        RemoveAllIngredients(true);

        cancelButton.gameObject.SetActive(false);
    }

    public void ImportIngredientMode()
    {
        AudioManager.PlaySound(1);
        productItemList.gameObject.SetActive(false);
        ingredientItemList.gameObject.SetActive(true);
        ingredientItemList.DisplayNewFilter("ingredient");
        cancelButton.gameObject.SetActive(true);
    }

    bool isRecipeMet()
    {
        if (productItem == null)
            return false;
        foreach (string ingredient in productItem.recipe.Keys)
        {
            if (addedIngredients[ingredient] < productItem.recipe[ingredient])
                return false;
        }

        return true;
    }

    public void CreateWorkOrder(bool isRandom)
    {
        MasterGameManager.instance.workOrderManager.CreateWorkOrder(productItem, isRandom);
        RemoveAllIngredients(false);
        if(isRandom)
            AudioManager.PlaySound(4);
        else
            AudioManager.PlaySound(3);
        ImportProductMode();
    }

    string ParseRecipe(Dictionary<string, int> recipe)
    {
        string result = "";
        foreach (var kvp in recipe)
        {
            result += string.Format("{0} x{1}\n", kvp.Key, kvp.Value);
            addedIngredients.Add(kvp.Key, 0);
        }

        result.Trim();

        return result;
    }

    void AddProduct(Item item)
    {
        productItem = item;
        productName.text = productItem.name;
        productIcon.color = Color.white;
        productIcon.sprite = productItem.icon;
        recipeText.text = ParseRecipe(productItem.recipe);

        ImportIngredientMode();
    }

    void AddIngredient(Item item)
    {
        if (PlayerInventory.inventory.GetItemCount(item) > 0 &&
            productItem.recipe.ContainsKey(item.name) &&
            addedIngredients[item.name] < productItem.recipe[item.name])
        {
            PlayerInventory.inventory.SubtractItem(item);
            addedIngredients[item.name]++;
            AudioManager.PlaySound(2);
            foreach (IngredientEntry entry in ingredientEntryList)
            {
                if (entry.loadedButton == null)
                {
                    entry.SpawnIngredientButton(item);
                    return;
                }
            }
        }
    }

    public void RemoveIngredient(IngredientEntry entry, bool restockInventory)
    {
        if (entry.loadedButton == null)
            return;

        Item item = entry.loadedButton.item;

        addedIngredients[item.name]--;
        if (restockInventory)
            PlayerInventory.inventory.AddItem(item);
        Destroy(entry.loadedButton.gameObject);
        entry.loadedButton = null;
    }

    void RemoveAllIngredients(bool restockInventory)
    {
        foreach (IngredientEntry entry in ingredientEntryList)
        {
            RemoveIngredient(entry, restockInventory);
        }
        addedIngredients.Clear();
    }

    public void RecipeCancelButton() 
    {
        AudioManager.PlaySound(0);
        ImportProductMode();
    }

    //void ProductButtonBehavior(InventoryButton invButton)
    //{
    //    invButton.ClickFunction = AddProduct;
    //}

    //void IngredientButtonBehavior(InventoryButton invButton)
    //{
    //    invButton.ClickFunction = AddIngredient;
    //}
}