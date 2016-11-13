using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransactionBoard : MonoBehaviour {
    Item item;

    public Text transactionItemName;
    public Image transactionItemImage;
    public Text transactionPrice;
    public AttributeBarUI fireAttribute, waterAttribute, earthAttribute, airAttribute;
    public Button sellButton, buyButton;

    public void DisplayItem(Item item, ShopUIManager.TransactionType transactionType)
    {
        this.item = item;
        transactionItemName.text = item.name;
        transactionItemImage.sprite = item.icon;
        transactionPrice.text = item.price.ToString();
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
        TradeManager.BuyItem(item, 1);
    }

    public void ClickSell()
    {
        TradeManager.SellItem(item, 1);
    }
}
