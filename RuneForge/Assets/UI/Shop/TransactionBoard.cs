using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransactionBoard : MonoBehaviour {
    private AudioManager AudioManager;
    private ShopUIManager shopManager;

    public Item item;
    public ShopUIManager.TransactionType transactionMode;

    public Text transactionItemName;
    public Image transactionItemImage;
    public Text transactionPrice;
    public TransactionQuanitity transactionQuanitity;
    public AttributeBarUI fireAttribute, waterAttribute, earthAttribute, airAttribute;
    public Button sellButton, buyButton;

    void Awake()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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

        //This needs to be refactored
        if (item.isIngredient)
        {
            fireAttribute.gameObject.SetActive(true);
            waterAttribute.gameObject.SetActive(true);
            earthAttribute.gameObject.SetActive(true);
            airAttribute.gameObject.SetActive(true);

            int level, value;

            level = (item.providedAttributes.TryGetValue("Fire", out value)) ? value : 0;
            fireAttribute.RefreshBar(level);

            level = (item.providedAttributes.TryGetValue("Water", out value)) ? value : 0;
            waterAttribute.RefreshBar(level);

            level = (item.providedAttributes.TryGetValue("Earth", out value)) ? value : 0;
            earthAttribute.RefreshBar(level);

            level = (item.providedAttributes.TryGetValue("Air", out value)) ? value : 0;
            airAttribute.RefreshBar(level);
        }
    }

    public void ClickBuy()
    {
        bool refreshPlayerInventory = PlayerInventory.inventory.GetItemCount(item) == 0;
        TradeManager.BuyItem(item, transactionQuanitity.quantity);
        if (refreshPlayerInventory)
            shopManager.sellItemList.RefreshPage();
        AudioManager.PlaySound(6);
    }

    public void ClickSell()
    {
        TradeManager.SellItem(item, transactionQuanitity.quantity);
        if (PlayerInventory.inventory.GetItemCount(item) == 0)
            shopManager.sellItemList.RefreshPage();
        AudioManager.PlaySound(5);
    }
}
