using UnityEngine;
using System.Collections;

public static class PlayerInventory {
    public static Inventory inventory = new Inventory();

    static PlayerInventory()
    {
        inventory.AddItem("Money", 1000);
    }
}
