using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopUIManager : MonoBehaviour {
    public enum TransactionType
    {
        BUY,
        SELL
    }
    private AudioManager audioManager;
    public ItemListUI buyItemList, sellItemList;
    public TransactionBoard transactionBoard;
    public Text moneyText;
    public GameObject menuBar;

    void Awake()
    {
        buyItemList.AddButtonFunction(BuyShopItemButton);
        sellItemList.AddButtonFunction(SellShopItemButton);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Enable(bool active)
    {
        this.gameObject.SetActive(active);
        MasterGameManager.instance.uiManager.uiOpen = active;
        MasterGameManager.instance.interactionManager.canInteract = !active;
        menuBar.SetActive(!active);
        if (active)
            audioManager.PlaySound(9);
        else
            audioManager.PlaySound(10);
    }

    void Update()
    {
        moneyText.text = PlayerInventory.money.ToString();
    }
    
    void ClickItemButton(Item item, TransactionType transactionType)
    {
        audioManager.PlaySound(1);
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
