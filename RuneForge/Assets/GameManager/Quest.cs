using UnityEngine;
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
        List<Item> productList = ItemCollection.FilterItemList("product");
        int randomProduct = Random.Range(0, productList.Count);        
        List<Item> ingredientList = ItemCollection.FilterItemList("ingredient");
        int randomIngredient = Random.Range(0, ingredientList.Count);

        //Sets the variables now.
        this.product = productList[randomProduct];
        if (product.Class == "Rune")
            deadlineDate = MasterGameManager.instance.actionClock.Day + 2;
        else if (product.Class == "Product")
            deadlineDate = MasterGameManager.instance.actionClock.Day + 5;
        this.amountProduct = Random.Range(1, 3);        
        this.gold = Mathf.FloorToInt((product.price * amountProduct * 1.25f)/10) * 10;
        this.ingredient = ingredientList[randomIngredient];
        this.amountIngredient = Random.Range(1, 6);

    }
}