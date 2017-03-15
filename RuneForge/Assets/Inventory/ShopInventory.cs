using UnityEngine;
using System.Collections;

public static class ShopInventory {
    public static Inventory inventory = new Inventory();

    static ShopInventory()
    {
        ///NOTICE: Have to find a way to tag shopStock as infinite,
        /// Perhaps another xml sheet indicating shopStock at certain times

        foreach (Item material in ItemCollection.FilterItemList("all"))
        {
            //if(material.level <= 1)
                inventory.SetItemCount(material, int.MaxValue);
        }
    }
}
