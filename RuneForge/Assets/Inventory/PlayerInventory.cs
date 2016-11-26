using UnityEngine;
using System.Collections;

public static class PlayerInventory {
    public static Inventory inventory = new Inventory();
    public static int money = 600;

    static PlayerInventory()
    {
        //inventory.SetItemCount("Money", 600);
        //Testing
        //foreach (Item item in ItemCollection.itemList)
        //{
        //    inventory.SetItemCount(item, int.MaxValue);
        //}
    }
}
