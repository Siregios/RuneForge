using UnityEngine;
using System.Collections;

public static class PlayerInventory {
    public static Inventory inventory = new Inventory();

    static PlayerInventory()
    {
        inventory.SetItemCount("Money", 250);

        //Testing
        inventory.SetItemCount("Sphere", 2);
        inventory.SetItemCount("Salamanders_Scale", 2);
    }
}
