using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecipePage : MonoBehaviour {
    private RecipeUIManager recipeManager;

    public Text productName;
    public Image productIcon;
    public RectTransform recipeArea;
    public RecipeCharm recipeCharm;
    //public float padX, padY;
    float areaWidth, areaHeight, charmWidth = 35, charmHeight = 10;
    //public Text recipeText;
    List<RecipeCharm> recipeCharmList = new List<RecipeCharm>();

    void Awake()
    {
        areaWidth = recipeArea.rect.width;
        areaHeight = recipeArea.rect.height;
        charmWidth = recipeCharm.GetComponent<RectTransform>().rect.width;
        charmHeight = recipeCharm.GetComponent<RectTransform>().rect.height;
    }

    public void SetProduct(Item item)
    {
        productName.text = item.name;
        productIcon.color = Color.white;
        productIcon.sprite = item.icon;
        //recipeText.text = RecipeToString(item.recipe);
        DisplayRecipe(item.recipe);
    }

    public void Clear()
    {
        productName.text = "";
        productIcon.color = Color.clear;
        //recipeText.text = "";
        ClearRecipe();
    }

    void DisplayRecipe(Dictionary<string, int> recipe)
    {
        int i = 0;
        foreach (var kvp in recipe)
        {
            float xPos = (i % 2) * (areaWidth - (charmWidth * (i % 2)));
            float yPos = -Mathf.Floor(i / 2) * (areaHeight / 3);
            RecipeCharm newRecipeCharm = (RecipeCharm)Instantiate(recipeCharm, recipeArea.transform);
            newRecipeCharm.transform.localScale = Vector3.one;
            newRecipeCharm.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, yPos, 0);
            newRecipeCharm.Initialize(kvp.Key, kvp.Value);

            recipeCharmList.Add(newRecipeCharm);
            i++;
        }
    }

    void ClearRecipe()
    {
        foreach(RecipeCharm charm in recipeCharmList)
        {
            Destroy(charm.gameObject);
        }
        recipeCharmList.Clear();
    }

    //string RecipeToString(Dictionary<string, int> recipe)
    //{
    //    string result = "";
    //    foreach (var kvp in recipe)
    //    {
    //        result += string.Format("{0} x{1}\n", kvp.Key, kvp.Value);
    //    }

    //    result.Trim();

    //    return result;
    //}
}
