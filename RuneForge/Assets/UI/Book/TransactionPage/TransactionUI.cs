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

    TransactionTab currentActionTab = TransactionTab.ShopStock;
    Inventory currentInventory;
    Item currentItem;

    void Awake()
    {
        currentInventory = ShopInventory.inventory;

        actionName = this.transform.Find("Name").GetComponent<Text>();
        actionIcon = this.transform.Find("IconPanel/Icon").GetComponent<Image>();
        actionPrice = this.transform.Find("PricePanel/Price").GetComponent<Text>();
        actionPrice.transform.parent.gameObject.SetActive(false);
        actionButton = this.transform.Find("ActionButton").GetComponent<Button>();

        invListUI = this.transform.parent.Find("InventoryPanel").GetComponent<InventoryListUI>();
        invListUI.ModifyAllButtons(ButtonBehavior);
    }

    void OnEnable()
    {
        MasterGameManager.instance.uiManager.uiOpen = true;
    }

    void OnDisable()
    {
        MasterGameManager.instance.uiManager.uiOpen = false;
    }

    // Tell inventory buttons spawned by invListUI to act in this manner.
    void ButtonBehavior(InventoryButton invButton)
    {
        invButton.ClickFunction = LoadItem;
        invButton.currentInventory = currentInventory;
    }


    /// <summary>
    /// everytime I change inventories, I should modify the button setups
    /// </summary>
    public void ClickYourStock()
    {
        currentActionTab = TransactionTab.YourStock;
        currentInventory = PlayerInventory.inventory;
        invListUI.ModifyAllButtons(ButtonBehavior);
        //ClearActionPanel();
    }

    public void ClickShopStock()
    {
        currentActionTab = TransactionTab.ShopStock;
        currentInventory = ShopInventory.inventory;
        invListUI.ModifyAllButtons(ButtonBehavior);
        //ClearActionPanel();
    }

    public void LoadItem(Item item)
    {
        currentItem = item;

        actionName.text = currentItem.name;
        actionIcon.sprite = currentItem.icon;
        actionIcon.color = Color.white;
        actionPrice.transform.parent.gameObject.SetActive(true);
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
        actionPrice.transform.parent.gameObject.SetActive(false);
        actionButton.interactable = false;
    }
}
