using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecipePage : MonoBehaviour {
    private RecipeUIManager recipeManager;

    public Text productName;
    public Image productIcon;
    public Text recipeText;

    public void SetProduct(Item item)
    {
        productName.text = item.name;
        productIcon.color = Color.white;
        productIcon.sprite = item.icon;
        //recipeText.text = RecipeToString(item.recipe);
    }

    public void Clear()
    {
        productName.text = "";
        productIcon.color = Color.clear;
        //recipeText.text = "";
    }

    string RecipeToString(Dictionary<string, int> recipe)
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
