using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransactionBoard : MonoBehaviour {
    public Item item;
    public ShopUIManager.TransactionType transactionMode;

    public Text transactionItemName;
    public Image transactionItemImage;
    public Text transactionPrice;
    public TransactionQuanitity transactionQuanitity;
    public AttributeBarUI fireAttribute, waterAttribute, earthAttribute, airAttribute;
    public Button sellButton, buyButton;

    void Update()
    {
        if (item != null)
        {
            if (transactionMode == ShopUIManager.TransactionType.SELL)
                sellButton.interactable = !(PlayerInventory.inventory.GetItemCount(item.name) <= 0);
            if (transactionMode == ShopUIManager.TransactionType.BUY)
                buyButton.interactable = !(ShopInventory.inventory.GetItemCount(item.name) <= 0);
        }
    }

    public void DisplayItem(Item item, ShopUIManager.TransactionType transactionType)
    {
        this.item = item;
        this.transactionMode = transactionType;
        transactionItemName.text = item.name;
        transactionItemImage.sprite = item.icon;
        transactionPrice.text = item.price.ToString();
        transactionQuanitity.SetQuantity(1);
        switch (transactionType)
        {
            case ShopUIManager.TransactionType.SELL:
                sellButton.gameObject.SetActive(true);
                buyButton.gameObject.SetActive(false);
                break;
            case ShopUIManager.TransactionType.BUY:
                buyButton.gameObject.SetActive(true);
                sellButton.gameObject.SetActive(false);
                break;
        }
        if (item.isIngredient)
        {
            fireAttribute.gameObject.SetActive(true);
            waterAttribute.gameObject.SetActive(true);
            earthAttribute.gameObject.SetActive(true);
            airAttribute.gameObject.SetActive(true);

            fireAttribute.RefreshBar(item.providedAttributes["Fire"]);
            waterAttribute.RefreshBar(item.providedAttributes["Water"]);
            earthAttribute.RefreshBar(item.providedAttributes["Earth"]);
            airAttribute.RefreshBar(item.providedAttributes["Air"]);
        }
    }

    public void ClickBuy()
    {
        TradeManager.BuyItem(item, transactionQuanitity.quantity);
    }

    public void ClickSell()
    {
        TradeManager.SellItem(item, transactionQuanitity.quantity);
    }
}
