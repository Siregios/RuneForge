﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RecipeUIManager : MonoBehaviour {
    Item productItem;

    public AudioClip randomOrderSound;
    public AudioClip normalOrderSound;
    public AudioClip addIngredientSound;
    public AudioClip productSelectSound;
    public AudioClip backSound;
    public ItemListUI productItemList, ingredientItemList;
    public RecipePage recipePage;
    public Button cancelButton, pinSelectButton, pinRandomButton;

    public List<IngredientEntry> ingredientEntryList;
    Dictionary<string, int> addedIngredients = new Dictionary<string, int>();
    Dictionary<string, int> providedAttributes = new Dictionary<string, int>()
    {
        { "Fire", 0 },
        { "Water", 0 },
        { "Earth", 0 },
        { "Air", 0 }
    };

    void Awake()
    {
        productItemList.AddButtonFunction(AddProduct);
        ingredientItemList.AddButtonFunction(AddIngredient);
    }

    void OnEnable()
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
        if (MasterGameManager.instance.upgradeManager.level1 == 1 || MasterGameManager.instance.upgradeManager.level1 == 3)
        {
            providedAttributes["Fire"] = 1;
            providedAttributes["Water"] = 1;
        }
        if (MasterGameManager.instance.upgradeManager.level1 == 2 || MasterGameManager.instance.upgradeManager.level1 == 3)
        {
            providedAttributes["Air"] = 1;
            providedAttributes["Earth"] = 1;
        }
        ingredientItemList.gameObject.SetActive(false);
        productItemList.gameObject.SetActive(true);
        productItem = null;
        RemoveAllIngredients(true);
        recipePage.Clear();
        EnablePinButtons(false);
        cancelButton.gameObject.SetActive(false);
    }

    public void ImportIngredientMode()
    {
        productItemList.gameObject.SetActive(false);
        ingredientItemList.gameObject.SetActive(true);
        ingredientItemList.ModifyDefaultFilter(recipeStringFilter(productItem.recipe));
        recipePage.ProvideToAttrBars(providedAttributes);
        EnablePinButtons(!MasterGameManager.instance.workOrderManager.IsFull());
        cancelButton.gameObject.SetActive(true);
    }

    string recipeStringFilter(Dictionary<string, int> recipe)
    {
        string resultFilter = "Ingredient:";

        foreach (string key in recipe.Keys)
        {
            resultFilter += string.Format("{0},", key);
        }

        if (resultFilter[resultFilter.Length - 1] == ',')
            resultFilter = resultFilter.Substring(0, resultFilter.Length - 1); //Remove the last comma at the end.

        return resultFilter;
    }

    bool AttributesMet()
    {
        if (productItem == null)
            return false;
        foreach (string attribute in productItem.requiredAttributes.Keys)
        {
            if (providedAttributes[attribute] < productItem.requiredAttributes[attribute])
                return false;
        }
        return true;
    }

    public void CreateWorkOrder(bool isRandom)
    {
        if (PlayerInventory.money >= recipePage.pinCost)    //Replace with productItem.pinCost if that variable is ever made
        {
            PlayerInventory.money -= recipePage.pinCost;
            bool enhanced = false;

            enhanced = AttributesMet();

            MasterGameManager.instance.workOrderManager.CreateWorkOrder(productItem, enhanced, isRandom);
            RemoveAllIngredients(false);
            if (isRandom)
                MasterGameManager.instance.audioManager.PlaySFXClip(randomOrderSound);
            else
                MasterGameManager.instance.audioManager.PlaySFXClip(normalOrderSound);
            ImportProductMode();
        }
        else
        {
            Debug.LogWarning("Not enough gold to create item");
        }
    }

    public void CreateWorkOrderTutorial()
    {
        MasterGameManager.instance.workOrderManager.CreateWorkOrderTutorial(productItem);
        MasterGameManager.instance.audioManager.PlaySFXClip(normalOrderSound);
        ImportProductMode();
    }

    void AddProduct(Item item)
    {
        MasterGameManager.instance.audioManager.PlaySFXClip(productSelectSound);
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
            ingredientItemList.RefreshPage();
            addedIngredients[item.ingredientType]++;
            foreach (var kvp in item.providedAttributes)
            {
                providedAttributes[kvp.Key] += kvp.Value;
            }
            recipePage.ProvideToAttrBars(providedAttributes);

            MasterGameManager.instance.audioManager.PlaySFXClip(addIngredientSound);
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
        foreach (var kvp in item.providedAttributes)
        {
            providedAttributes[kvp.Key] -= kvp.Value;
        }
        recipePage.ProvideToAttrBars(providedAttributes);
        if (restockInventory)
        {
            PlayerInventory.inventory.AddItem(item);
            ingredientItemList.RefreshPage();
        }
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
        MasterGameManager.instance.audioManager.PlaySFXClip(backSound);
        ImportProductMode();
    }
}