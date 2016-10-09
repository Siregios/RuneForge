using UnityEngine;
using System.Collections;

public class TradeManager : MonoBehaviour {
    public static void BuyItem(Item item, int count)
    {

        int finalCount = count;

        if (PlayerInventory.inventory.GetItemCount("Money") < item.price * count)
        {
            finalCount = count - Mathf.CeilToInt(((count * item.price) - PlayerInventory.inventory.GetItemCount("Money")) / (float)item.price);
        }
        else if (ShopInventory.inventory.GetItemCount(item.name) - count < 0)
        {
            finalCount = ShopInventory.inventory.GetItemCount(item.name);
        }

        ShopInventory.inventory.SubtractItem(item.name, finalCount);
        PlayerInventory.inventory.SubtractItem("Money", finalCount * item.price);
        PlayerInventory.inventory.AddItem(item.name, finalCount);
    }

    public static void SellItem(Item item, int count)
    {

        int finalCount = count;

        if (PlayerInventory.inventory.GetItemCount(item.name) < count)
        {
            finalCount = PlayerInventory.inventory.GetItemCount(item.name);
        }

        PlayerInventory.inventory.AddItem("Money", finalCount * item.price);
        PlayerInventory.inventory.SubtractItem(item.name, finalCount);
    }
}
