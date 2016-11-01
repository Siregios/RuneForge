using UnityEngine;
using System.Collections;

public static class PlayerInventory {
    public static Inventory inventory = new Inventory();

    static PlayerInventory()
    {
        inventory.SetItemCount("Money", 250);
    }
}
