using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransactionUI : MonoBehaviour {
    enum TransactionTab
    {
        YourStock,
        ShopStock
    }
    InventoryListUI invListUI;

    Button actionButton;
    Text actionName, actionPrice;
    Image actionIcon;

    TransactionTab currentActionTab = TransactionTab.YourStock;
    Inventory currentInventory;
    Item currentItem;

    void Awake()
    {
        currentInventory = PlayerInventory.inventory;

        actionName = this.transform.FindChild("Name").GetComponent<Text>();
        actionIcon = this.transform.FindChild("Icon").GetComponent<Image>();
        actionPrice = this.transform.FindChild("Price").GetComponent<Text>();
        actionButton = this.transform.FindChild("ActionButton").GetComponent<Button>();

        invListUI = this.transform.parent.FindChild("InventoryPanel").GetComponent<InventoryListUI>();
        invListUI.ModifyAllButtons(ButtonBehavior);
    }

    // Tell inventory buttons spawned by invListUI to act in this manner.
    void ButtonBehavior(InventoryButton invButton)
    {
        invButton.ClickFunction = LoadItem;
        if (currentInventory.GetItemCount(invButton.item.name) == int.MaxValue)
            invButton.countText.text = "∞";
        else
            invButton.countText.text = "x" + currentInventory.GetItemCount(invButton.item.name).ToString();
    }


    /// <summary>
    /// everytime I change inventories, I should modify the button setups
    /// </summary>
    public void ClickYourStock()
    {
        currentActionTab = TransactionTab.YourStock;
        currentInventory = PlayerInventory.inventory;
        invListUI.ModifyAllButtons(ButtonBehavior);
        ClearActionPanel();
    }

    public void ClickShopStock()
    {
        currentActionTab = TransactionTab.ShopStock;
        currentInventory = ShopInventory.inventory;
        invListUI.ModifyAllButtons(ButtonBehavior);
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

        invListUI.ModifyAllButtons(ButtonBehavior);
    }

    void ClearActionPanel()
    {
        actionName.text = "";
        actionIcon.color = Color.clear;
        actionPrice.text = "";
        actionButton.interactable = false;
    }
}
