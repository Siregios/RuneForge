using UnityEngine;
using System.Collections;

public static class ShopInventory {
    public static Inventory inventory = new Inventory();

    static ShopInventory()
    {
        foreach (Item material in ItemCollection.materialList)
        {
            inventory.SetItemCount(material.name, int.MaxValue);
        }
    }
}
