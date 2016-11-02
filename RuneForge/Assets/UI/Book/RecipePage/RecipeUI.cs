using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecipeUI : MonoBehaviour {
    Item productItem;

    public Button cancelButton, pinSelectButton, pinRandomButton;
    InventoryListUI invListUI;
    Text productName;
    Image productIcon;
    Text recipeText;
    GameObject recipeArea;

    //Refactor this later?
    //Roldan: Yes
    //Peter & Efren: Same
    //Edwin: FUCK
    public List<IngredientEntry> ingredientEntryList;
    Dictionary<string, int> addedIngredients;

    void Awake()
    {
        invListUI = this.transform.parent.Find("InventoryPanel").GetComponent<InventoryListUI>();

        productName = this.transform.Find("ProductName").GetComponent<Text>();
        productIcon = this.transform.Find("ProductIconPanel/ProductIcon").GetComponent<Image>();
        recipeText = this.transform.Find("Recipe").GetComponent<Text>();
        recipeArea = this.transform.Find("RecipeArea").gameObject;

        invListUI.ModifyAllButtons(ProductButtonBehavior);
    }

    void Start()
    {
        ImportProductMode();
    }
    
    void Update()
    {
        if (isRecipeMet())
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
        invListUI.ModifyAllButtons(ProductButtonBehavior);
        invListUI.DisplayNewFilter("product");

        productItem = null;
        productName.text = "";
        productIcon.color = Color.clear;
        recipeText.text = "";
        foreach(IngredientEntry entry in ingredientEntryList)
        {
            entry.ClearButton();
        }
        addedIngredients = new Dictionary<string, int>();

        cancelButton.gameObject.SetActive(false);
    }

    public void ImportIngredientMode()
    {
        invListUI.ModifyAllButtons(IngredientButtonBehavior);
        invListUI.DisplayNewFilter("ingredient");
        cancelButton.gameObject.SetActive(true);
    }

    void ProductButtonBehavior(InventoryButton invButton)
    {
        invButton.ClickFunction = AddProduct;
    }

    void IngredientButtonBehavior(InventoryButton invButton)
    {
        invButton.ClickFunction = AddIngredient;
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
        if (PlayerInventory.inventory.GetItemCount(item.name) > 0 &&
            productItem.recipe.ContainsKey(item.name) && 
            addedIngredients[item.name] < productItem.recipe[item.name])
        {
            PlayerInventory.inventory.SubtractItem(item.name);
            addedIngredients[item.name]++;
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

    public void RemoveIngredient(Item item)
    {
        addedIngredients[item.name]--;
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
}