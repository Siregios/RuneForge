﻿using UnityEngine;
using System.Collections;

public static class PlayerInventory {
    public static Inventory inventory = new Inventory();
    public static int money = 999999;

    static PlayerInventory()
    {
        //Testing
        foreach (Item item in ItemCollection.itemList)
        {
            inventory.SetItemCount(item, 10);
        }
    }

    //static GodwinHo()
    //{
    //    //Edwin is God
    //    foreach (Item item in ItemCollection.itemList)
    //    {
    //        inventory.SetItemCount(item, 694201337);
    //    }
    //    money = int.Parse("xXxEdgeWin420BestReaperQNA");
    //}
}
