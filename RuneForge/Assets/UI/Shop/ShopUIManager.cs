using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopUIManager : MonoBehaviour {
    public enum TransactionType
    {
        BUY,
        SELL
    }
    private AudioManager AudioManager;
    public ItemListUI buyItemList, sellItemList;
    public TransactionBoard transactionBoard;
    public Text moneyText;
    public GameObject menuBar;

    void Awake()
    {
        buyItemList.AddButtonFunction(BuyShopItemButton);
        sellItemList.AddButtonFunction(SellShopItemButton);
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.uiOpen = active;
        MasterGameManager.instance.interactionManager.canInteract = !active;
        menuBar.SetActive(!active);
    }

    void Update()
    {
        //moneyText.text = PlayerInventory.inventory.GetItemCount("Money").ToString();
        moneyText.text = PlayerInventory.money.ToString();
    }
    
    void ClickItemButton(Item item, TransactionType transactionType)
    {
        AudioManager.PlaySound(1);
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
