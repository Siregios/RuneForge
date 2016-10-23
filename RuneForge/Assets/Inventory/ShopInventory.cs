using UnityEngine;
using System.Collections;

public static class ShopInventory {
    public static Inventory inventory = new Inventory();

    static ShopInventory()
    {
        ///NOTICE: Have to find a way to tag shopStock as infinite,
        /// Perhaps another xml sheet indicating shopStock at certain times
        

        //Testing with just chalk
        foreach (Item chalk in ItemCollection.FilterItem("Chalk"))
        {
            inventory.SetItemCount(chalk.name, int.MaxValue);
        }


        //foreach (Item material in ItemCollection.materialList)
        //{
        //    inventory.SetItemCount(material.name, int.MaxValue);
        //}
    }
}
