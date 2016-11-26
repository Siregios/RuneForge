using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecipeUIManager : MonoBehaviour {
    Item productItem;
    private AudioManager AudioManager;

    public ItemListUI productItemList, ingredientItemList;
    public RecipePage recipePage;
    public Button cancelButton, pinSelectButton, pinRandomButton;

    public List<IngredientEntry> ingredientEntryList;
    Dictionary<string, int> addedIngredients = new Dictionary<string, int>();

    void Awake()
    {
        productItemList.AddButtonFunction(AddProduct);
        ingredientItemList.AddButtonFunction(AddIngredient);
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.uiOpen = active;
        MasterGameManager.instance.interactionManager.canInteract = !active;
    }

    void Start()
    {
        ImportProductMode();
    }

    void EnablePinButtons(bool active)
    {
        pinSelectButton.interactable = active;
        pinRandomButton.interactable = active;
    }

    public void ImportProductMode()
    {
        ingredientItemList.gameObject.SetActive(false);
        
        productItemList.gameObject.SetActive(true);
        productItemList.DisplayNewFilter("Product");
        productItem = null;
        recipePage.Clear();
        RemoveAllIngredients(true);
        EnablePinButtons(false);
        cancelButton.gameObject.SetActive(false);
    }

    public void ImportIngredientMode()
    {
        productItemList.gameObject.SetActive(false);
        ingredientItemList.gameObject.SetActive(true);
        ingredientItemList.DisplayNewFilter("Ingredient");
        EnablePinButtons(!MasterGameManager.instance.workOrderManager.IsFull());
        cancelButton.gameObject.SetActive(true);
    }

    //bool isRecipeMet()
    //{
    //    if (productItem == null)
    //        return false;
    //    foreach (string ingredient in productItem.recipe.Keys)
    //    {
    //        if (addedIngredients[ingredient] < productItem.recipe[ingredient])
    //            return false;
    //    }
    //    return true;
    //}

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

    void AddProduct(Item item)
    {
        AudioManager.PlaySound(1);
        productItem = item;
        foreach (var kvp in item.recipe)
        {
            addedIngredients.Add(kvp.Key, 0);
        }
        recipePage.SetProduct(item);
        ImportIngredientMode();
    }

    void AddIngredient(Item item)
    {
        if (PlayerInventory.inventory.GetItemCount(item) > 0 &&
            productItem.recipe.ContainsKey(item.ingredientType) &&
            addedIngredients[item.ingredientType] < productItem.recipe[item.ingredientType])
        {
            PlayerInventory.inventory.SubtractItem(item);
            addedIngredients[item.ingredientType]++;
            AudioManager.PlaySound(2);
            foreach (IngredientEntry entry in ingredientEntryList)
            {
                if (entry.loadedButton == null)
                {
                    entry.SpawnIngredientButton(item, this);
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

        addedIngredients[item.ingredientType]--;
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

    public void BackButton() 
    {
        AudioManager.PlaySound(0);
        ImportProductMode();
    }
}