using UnityEngine;
using System.Collections;

public class TradeManager : MonoBehaviour {
    public static void BuyItem(Item item, int count)
    {

        int finalCount = count;

        if (!CanBuy(item, count))
        {
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

        if (!CanSell(item, count))
        {
            finalCount = PlayerInventory.inventory.GetItemCount(item);
        }
        if (MasterGameManager.instance.upgradeManager.level3 == 1 || MasterGameManager.instance.upgradeManager.level3 == 3)
            PlayerInventory.money += finalCount * item.price;
        else
            PlayerInventory.money += finalCount * item.price/2;
        PlayerInventory.inventory.SubtractItem(item, finalCount);
    }

    public static bool CanBuy(Item item, int count)
    {
        return PlayerInventory.money >= item.price * count;
    }

    public static bool CanSell(Item item, int count)
    {
        return PlayerInventory.inventory.GetItemCount(item) >= count;
    }
}
