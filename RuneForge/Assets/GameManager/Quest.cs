﻿using UnityEngine;
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
}