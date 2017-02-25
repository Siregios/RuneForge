using UnityEngine;
using System.Collections;

public static class PlayerInventory {
    public static Inventory inventory = new Inventory();
    public static int money = 600;

    static PlayerInventory()
    {
        //Testing
        foreach (Item item in ItemCollection.itemList)
        {
            inventory.SetItemCount(item, int.MaxValue);
        }
    }
}
