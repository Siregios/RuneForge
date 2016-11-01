using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecipeUI : MonoBehaviour {
    Item productItem;

    public Button cancelButton;
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

    void Awake()
    {
        invListUI = this.transform.parent.FindChild("InventoryPanel").GetComponent<InventoryListUI>();

        productName = this.transform.FindChild("ProductName").GetComponent<Text>();
        productIcon = this.transform.FindChild("ProductIcon").GetComponent<Image>();
        recipeText = this.transform.FindChild("Recipe").GetComponent<Text>();
        recipeArea = this.transform.FindChild("RecipeArea").gameObject;
        ImportProductMode();
    }

    public void ImportProductMode()
    {
        productName.text = "";
        productIcon.color = Color.clear;
        recipeText.text = "";
        foreach(IngredientEntry entry in ingredientEntryList)
        {
            entry.ClearButton();
        }

        invListUI.ModifyAllButtons(ProductButtonBehavior);
        invListUI.DisplayNewFilter("product");
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
        if (productItem.recipe.ContainsKey(item.name))
        {
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

    string ParseRecipe(Dictionary<string, int> recipe)
    {
        string result = "";
        foreach (var kvp in recipe)
        {
            result += string.Format("{0} x{1}\n", kvp.Key, kvp.Value);
        }

        result.Trim();

        return result;
    }
}