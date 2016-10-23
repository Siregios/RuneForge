using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransactionUI : MonoBehaviour {
    enum TransactionTab
    {
        YourStock,
        ShopStock
    }
    public InventoryListUI invListUI;

    Button actionButton;
    Text actionName, actionPrice;
    Image actionIcon;

    TransactionTab currentActionTab = TransactionTab.YourStock;
    Item currentItem;

    void Awake()
    {
        actionName = this.transform.FindChild("Name").GetComponent<Text>();
        actionIcon = this.transform.FindChild("Icon").GetComponent<Image>();
        actionPrice = this.transform.FindChild("Price").GetComponent<Text>();
        actionButton = this.transform.FindChild("ActionButton").GetComponent<Button>();
    }

    public void ClickYourStock()
    {
        currentActionTab = TransactionTab.YourStock;
        invListUI.ChangeInventory(PlayerInventory.inventory);
        ClearActionPanel();
    }

    public void ClickShopStock()
    {
        currentActionTab = TransactionTab.ShopStock;
        invListUI.ChangeInventory(ShopInventory.inventory);
        ClearActionPanel();
    }

    public void LoadItem(Item item)
    {
        currentItem = item;

        actionName.text = currentItem.name;
        actionIcon.sprite = currentItem.icon;
        actionIcon.color = Color.white;
        actionPrice.text = ItemCollection.itemDict[currentItem.name].price.ToString();
        actionButton.interactable = true;
    }

    public void ClickTrade()
    {
        switch (currentActionTab)
        {
            //Selling
            case TransactionTab.YourStock:
                //Edit this for sell-x
                TradeManager.SellItem(currentItem, 1);
                break;

            //Buying
            case TransactionTab.ShopStock:
                TradeManager.BuyItem(currentItem, 1);
                break;
        }
    }

    void ClearActionPanel()
    {
        actionName.text = "";
        actionIcon.color = Color.clear;
        actionPrice.text = "";
        actionButton.interactable = false;
    }
}
