using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngredientButton : MonoBehaviour {
    public Item item;

    RecipeUI recipeUI;

    public void Initialize(Item item)
    {
        recipeUI = this.transform.parent.parent.parent.GetComponent<RecipeUI>();
        this.item = item;
        this.GetComponent<Image>().sprite = item.icon;
    }

    public void RemoveButton()
    {
        PlayerInventory.inventory.AddItem(this.item.name);
        recipeUI.RemoveIngredient(item);
        Destroy(this.gameObject);
    }
}
