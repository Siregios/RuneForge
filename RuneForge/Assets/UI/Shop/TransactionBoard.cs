﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransactionBoard : MonoBehaviour
{
    private ShopUIManager shopManager;

    public Item item;
    public ShopUIManager.TransactionType transactionMode = ShopUIManager.TransactionType.BUY;

    public Text transactionItemName;
    public Image transactionItemImage;
    public Text transactionPrice;
    public TransactionQuanitity transactionQuanitity;
    public AttrBarGroup attributeBars;
    //public AttributeBarUI fireAttribute, waterAttribute, earthAttribute, airAttribute;
    public Button sellButton, buyButton;

    public AudioClip goldLoseSound;
    public AudioClip goldGainSound;

    void Awake()
    {
        shopManager = this.transform.parent.GetComponent<ShopUIManager>();
    }

    void Update()
    {
        if (item != null)
        {
            if (transactionMode == ShopUIManager.TransactionType.SELL)
            {
                sellButton.interactable = !(PlayerInventory.inventory.GetItemCount(item) <= 0) && TradeManager.CanSell(item, transactionQuanitity.quantity);
            }
            if (transactionMode == ShopUIManager.TransactionType.BUY)
            {
                buyButton.interactable = !(ShopInventory.inventory.GetItemCount(item) <= 0) && TradeManager.CanBuy(item, transactionQuanitity.quantity);
            }
        }
    }

    public void DisplayItem(Item item, ShopUIManager.TransactionType transactionType)
    {
        this.item = item;
        this.transactionMode = transactionType;
        transactionItemName.text = item.name;
        transactionItemImage.sprite = item.icon;
        transactionItemImage.color = Color.white;
        transactionPrice.text = item.price.ToString();
        transactionQuanitity.SetQuantity(1);
        switch (transactionType)
        {
            case ShopUIManager.TransactionType.SELL:
                sellButton.gameObject.SetActive(true);
                buyButton.gameObject.SetActive(false);
                if (MasterGameManager.instance.upgradeManager.level3 == 1 || MasterGameManager.instance.upgradeManager.level3 == 3)
                    transactionPrice.text = item.price.ToString();
                else
                    transactionPrice.text = (item.price/2).ToString();
                break;
            case ShopUIManager.TransactionType.BUY:
                buyButton.gameObject.SetActive(true);
                sellButton.gameObject.SetActive(false);
                break;
        }

        attributeBars.Clear();
        foreach (var kvp in item.providedAttributes)
        {
            attributeBars.SetBar(kvp.Key, kvp.Value);
            attributeBars.SetText(kvp.Key, kvp.Value.ToString());
        }
    }

    public void ClickBuy()
    {
        bool refreshPlayerInventory = PlayerInventory.inventory.GetItemCount(item) == 0;
        TradeManager.BuyItem(item, transactionQuanitity.quantity);
        if (refreshPlayerInventory)
            shopManager.sellItemList.RefreshPage();
        MasterGameManager.instance.audioManager.PlaySFXClip(goldLoseSound);
    }

    public void ClickSell()
    {
        TradeManager.SellItem(item, transactionQuanitity.quantity);
        if (PlayerInventory.inventory.GetItemCount(item) == 0)
            shopManager.sellItemList.RefreshPage();
        MasterGameManager.instance.audioManager.PlaySFXClip(goldGainSound);
    }

    public void Clear()
    {
        this.item = null;
        transactionItemName.text = "";
        transactionItemImage.color = Color.clear;
        transactionPrice.text = "";
        transactionQuanitity.quantity = 0;
        attributeBars.Clear();
    }
}
