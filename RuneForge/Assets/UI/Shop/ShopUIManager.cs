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
    public AudioClip storeEntry;
    public AudioClip storeExit;
    public AudioClip itemSelectSound;

    void Awake()
    {
        buyItemList.AddButtonFunction(BuyShopItemButton);
        sellItemList.AddButtonFunction(SellShopItemButton);
    }

    public void Enable(bool active)
    {
        //this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.Enable(this.gameObject, active, true);
        if (active)
        {
            buyItemList.RefreshPage(true);
            sellItemList.RefreshPage(true);
            MasterGameManager.instance.audioManager.PlaySFXClip(storeEntry);
        }
        else
            MasterGameManager.instance.audioManager.PlaySFXClip(storeExit);
    }

    void Update()
    {
        moneyText.text = PlayerInventory.money.ToString();
    }
    
    void ClickItemButton(Item item, TransactionType transactionType)
    {
        MasterGameManager.instance.audioManager.PlaySFXClip(itemSelectSound);
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
