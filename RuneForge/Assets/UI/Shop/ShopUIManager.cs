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
    public GameObject menuBar;

    void Awake()
    {
        buyItemList.AddButtonFunction(BuyShopItemButton);
        sellItemList.AddButtonFunction(SellShopItemButton);
    }

    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.uiOpen = active;
        MasterGameManager.instance.interactionManager.canInteract = !active;
        menuBar.SetActive(!active);
    }

    //void OnEnable()
    //{
    //    MasterGameManager.instance.uiManager.uiOpen = true;
    //}

    //void OnDisable()
    //{
    //    MasterGameManager.instance.uiManager.uiOpen = false;
    //}

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
