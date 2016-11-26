using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngredientEntry : MonoBehaviour {
    public GameObject ingredientButton;
    
    public IngredientButton loadedButton = null;

    public void SpawnIngredientButton(Item item, RecipeUIManager recipeUI)
    {
        GameObject newButton = (GameObject)Instantiate(ingredientButton, this.transform.position, Quaternion.identity);
        newButton.transform.SetParent(this.transform);
        newButton.transform.localScale = Vector3.one;
        newButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

        IngredientButton newIngredientButton = newButton.GetComponent<IngredientButton>();
        newIngredientButton.Initialize(item, recipeUI);
        this.loadedButton = newIngredientButton;
    }
}
