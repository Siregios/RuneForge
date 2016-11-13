using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopUIManager : MonoBehaviour {
    public enum TransactionType
    {
        BUY,
        SELL
    }
    public ItemListUI buyItemList, sellItemList;
    public TransactionBoard transactionBoard;
    public Text moneyText;

    void Awake()
    {
        buyItemList.AddButtonFunction(BuyShopItemButton);
        sellItemList.AddButtonFunction(SellShopItemButton);
    }

    void Update()
    {
        moneyText.text = PlayerInventory.inventory.GetItemCount("Money").ToString();
    }
    
    void ClickItemButton(Item item, TransactionType transactionType)
    {
        transactionBoard.DisplayItem(item, transactionType);
    }

    void BuyShopItemButton(Item item)
    {
        ClickItemButton(item, TransactionType.BUY);
    }

    void SellShopItemButton(Item item)
    {
        ClickItemButton(item, TransactionType.SELL);
    }
}
