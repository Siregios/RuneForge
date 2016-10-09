using UnityEngine;
using System.Collections;

public class TradeManager : MonoBehaviour {
    public static void BuyItem(Item item, int amount)
    {
        //if (PlayerInventory.inventory.GetItemCount("Money") < item.price * amount)
        //{

        //}
        //else if (ShopInventory.inventory.GetItemCount(item.name))
        //{

        //}
        ShopInventory.inventory.SubtractItem(item.name, amount);
        PlayerInventory.inventory.SubtractItem("Money", item.price * amount);
        PlayerInventory.inventory.AddItem(item.name, amount);
    }

    public static void SellItem(Item item, int amount)
    {
        PlayerInventory.inventory.AddItem("Money", item.price * amount);
        PlayerInventory.inventory.SubtractItem(item.name, amount);
    }
}
