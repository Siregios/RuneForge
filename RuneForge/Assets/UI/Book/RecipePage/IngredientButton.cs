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
        //if (restockInventory)
        //    PlayerInventory.inventory.AddItem(this.item.name);
        recipeUI.RemoveIngredient(this.transform.parent.GetComponent<IngredientEntry>(), true);
        //Destroy(this.gameObject);
    }
}
