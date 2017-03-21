using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Quest {
    //This is the quest stuff
    public Item product;
    public int amountProduct;
    public int deadlineDate;

    //This is the rewards
    public int gold;
    public Item ingredient;
    public int amountIngredient;

    public Quest()
    {
        //Produces random product and random ingredient reward
        List<Item> productList = ItemCollection.FilterItemList("BaseProducts");
        int randomProduct = Random.Range(0, productList.Count);        
        List<Item> ingredientList = ItemCollection.FilterItemList("material");
        int randomIngredient = Random.Range(0, ingredientList.Count);

        //Sets the variables now.
        this.product = productList[randomProduct];
        if (product.Class == "Rune")
            deadlineDate = MasterGameManager.instance.actionClock.Day + 2;
        else if (product.Class == "Product")
            deadlineDate = MasterGameManager.instance.actionClock.Day + 5;
        this.amountProduct = Random.Range(1, 1);        
        this.gold = Mathf.FloorToInt((product.price * amountProduct * 1.25f)/10) * 10;
        this.ingredient = ingredientList[randomIngredient];
        this.amountIngredient = Random.Range(1, 6);
    }

    public Quest(Item product, int amount)
    {
        this.product = product;
        this.amountProduct = amount;
        this.gold = Mathf.FloorToInt((product.price * amountProduct * 1.25f) / 10) * 10;

        List<Item> ingredientList = ItemCollection.FilterItemList("material");
        int randomIngredient = Random.Range(0, ingredientList.Count);
        this.ingredient = ingredientList[randomIngredient];
        this.amountIngredient = Random.Range(1, 6);
    }

    public Quest(Item product, int productAmount, int deadlineDate, int gold, Item ingredient, int amountIngredient)
    {
        this.product = product;
        this.amountProduct = productAmount;
        this.deadlineDate = deadlineDate;
        this.gold = gold;
        this.ingredient = ingredient;
        this.amountIngredient = amountIngredient;
    }

    /// <summary>
    /// Returns the quest as a string formatted as: {productName}|{amountProduct(int)}|{deadlineDate(int)}|{gold(int)}|{ingredientName}|{amountIngredient(int)}
    /// </summary>
    /// <returns></returns>
    public static string SerializeToString(Quest quest)
    {
        string result = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
            quest.product.name,
            quest.amountProduct,
            quest.deadlineDate,
            quest.gold,
            quest.ingredient.name,
            quest.amountIngredient);

        return result;
    }

    public static Quest DeserialzeFromString(string questString)
    {
        List<string> values = questString.Split('|').ToList();
        Item productItem = ItemCollection.itemDict[values[0]];
        int amountProduct = System.Convert.ToInt32(values[1]);
        int deadlineDate = System.Convert.ToInt32(values[2]);
        int goldReward = System.Convert.ToInt32(values[3]);
        Item ingredient = ItemCollection.itemDict[values[4]];
        int amountIngredient = System.Convert.ToInt32(values[5]);

        return new Quest(productItem, amountProduct, deadlineDate, goldReward, ingredient, amountIngredient);
    }
}