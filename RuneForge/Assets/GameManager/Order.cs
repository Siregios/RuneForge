using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order {
    public Item item;
    public int deadlineDate;

    public Order()
    {
        List<Item> productList = ItemCollection.FilterItemList("product");
        int randomIndex = Random.Range(0, productList.Count);
        this.item = productList[randomIndex];
        if (item.Class == "Rune")
            deadlineDate = MasterGameManager.instance.actionClock.Day + 2;
        else if (item.Class == "Product")
            deadlineDate = MasterGameManager.instance.actionClock.Day + 5;
    }
}