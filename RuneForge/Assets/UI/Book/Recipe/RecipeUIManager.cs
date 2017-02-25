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
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
        EnablePinButtons(!MasterGameManager.instance.workOrderManager.IsFull());
        cancelButton.gameObject.SetActive(true);
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
        bool enhanced = false;

        enhanced = AttributesMet();

        MasterGameManager.instance.workOrderManager.CreateWorkOrder(productItem, enhanced, isRandom);
        RemoveAllIngredients(false);
        if(isRandom)
            AudioManager.PlaySound(4);
        else
            AudioManager.PlaySound(3);
        ImportProductMode();
    }

    public void CreateWorkOrderTutorial()
    {
        MasterGameManager.instance.workOrderManager.CreateWorkOrderTutorial(productItem);
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
            foreach (var kvp in item.providedAttributes)
            {
                providedAttributes[kvp.Key] += kvp.Value;
            }
            recipePage.ProvideToAttrBars(providedAttributes);

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
        foreach (var kvp in item.providedAttributes)
        {
            providedAttributes[kvp.Key] -= kvp.Value;
        }
        recipePage.ProvideToAttrBars(providedAttributes);
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