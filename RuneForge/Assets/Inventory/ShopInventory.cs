using UnityEngine;
using System.Collections;

public static class ShopInventory {
    public static Inventory inventory = new Inventory();

    static ShopInventory()
    {
        ///NOTICE: Have to find a way to tag shopStock as infinite,
        /// Perhaps another xml sheet indicating shopStock at certain times

        foreach (Item material in ItemCollection.FilterItem("material"))
        {
            inventory.SetItemCount(material.name, int.MaxValue);
        }
        //foreach (Item material in ItemCollection.FilterItem("fire"))
        //{
        //    inventory.SetItemCount(material.name, int.MaxValue);
        //}
        //foreach (Item material in ItemCollection.FilterItem("stone"))
        //{
        //    inventory.SetItemCount(material.name, int.MaxValue);
        //}
    }
}
