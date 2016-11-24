using UnityEngine;
using System.Collections;

public class TradeManager : MonoBehaviour {
    public static void BuyItem(Item item, int count)
    {

        int finalCount = count;

        //if (PlayerInventory.inventory.GetItemCount("Money") < item.price * count)
        if (PlayerInventory.money < item.price * count)
        {
            //finalCount = count - Mathf.CeilToInt(((count * item.price) - PlayerInventory.inventory.GetItemCount("Money")) / (float)item.price);
            finalCount = count - Mathf.CeilToInt(((count * item.price) - PlayerInventory.money) / (float)item.price);
        }
        else if (ShopInventory.inventory.GetItemCount(item) - count < 0)
        {
            finalCount = ShopInventory.inventory.GetItemCount(item);
        }
        ShopInventory.inventory.SubtractItem(item, finalCount);
        PlayerInventory.money -= finalCount * item.price;
        PlayerInventory.inventory.AddItem(item, finalCount);
    }

    public static void SellItem(Item item, int count)
    {

        int finalCount = count;

        if (PlayerInventory.inventory.GetItemCount(item) < count)
        {
            finalCount = PlayerInventory.inventory.GetItemCount(item);
        }

        //PlayerInventory.inventory.AddItem("Money", finalCount * item.price);
        PlayerInventory.money += finalCount * item.price;
        PlayerInventory.inventory.SubtractItem(item, finalCount);
    }
}
