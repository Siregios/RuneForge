using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngredientButton : MonoBehaviour {
    public Item item;

    RecipeUIManager recipeUI;

    public void Initialize(Item item, RecipeUIManager recipeUI)
    {
        this.item = item;
        this.recipeUI = recipeUI;
        this.GetComponent<Image>().sprite = item.icon;
    }

    public void RemoveButton()
    {
        recipeUI.RemoveIngredient(this.transform.parent.GetComponent<IngredientEntry>(), true);
    }
}
