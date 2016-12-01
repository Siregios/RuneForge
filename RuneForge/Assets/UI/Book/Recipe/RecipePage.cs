using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecipePage : MonoBehaviour {
    private RecipeUIManager recipeManager;
    private Item item;
    public Text productName;
    public Image productIcon;
    public RectTransform recipeArea;
    public RecipeCharm recipeCharm;
    public AttrBarGroup attributeBars;
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
        this.item = item;
        productName.text = item.name;
        productIcon.color = Color.white;
        productIcon.sprite = item.icon;
        DisplayRecipe(item.recipe);
        DisplayRequiredAttr(item.requiredAttributes);
    }

    public void Clear()
    {
        productName.text = "";
        productIcon.color = Color.clear;
        ClearRecipe();
        attributeBars.Clear();
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

    void DisplayRequiredAttr(Dictionary<string, int> reqAttributes)
    {
        foreach (var kvp in reqAttributes)
        {
            attributeBars.SetPlaceholder(kvp.Key, kvp.Value);
            attributeBars.SetText(kvp.Key, string.Format("0/" + kvp.Value));
        }
    }

    public void ProvideToAttrBars(Dictionary<string, int> provAttributes)
    {
        foreach (var kvp in provAttributes)
        {
            attributeBars.SetBar(kvp.Key, kvp.Value);
            attributeBars.SetText(kvp.Key, string.Format("{0}/{1}", kvp.Value, item.requiredAttributes[kvp.Key]));
        }
    }
}
