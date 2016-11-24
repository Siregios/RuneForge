using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransactionQuanitity : MonoBehaviour {
    public TransactionBoard transactionBoard;
    public InputField inputQuantity;
    public Button increaseQuantity, decreaseQuantity;
    public int quantity = 1;

    void Update()
    {
        increaseQuantity.interactable = !(quantity >= ItemCount());
        decreaseQuantity.interactable = !(quantity <= 1);
    }

    public void SetQuantity(int quantity)
    {
        this.quantity = quantity;
        if (transactionBoard.transactionMode == ShopUIManager.TransactionType.BUY &&
            //PlayerInventory.inventory.GetItemCount("Money") <= transactionBoard.item.price * this.quantity)
            PlayerInventory.money <= transactionBoard.item.price * this.quantity)
        {
            //this.quantity = PlayerInventory.inventory.GetItemCount("Money") / transactionBoard.item.price;
            this.quantity = PlayerInventory.money / transactionBoard.item.price;
        }
        else if (this.quantity <= 1)
            this.quantity = 1;
        else if (this.quantity >= ItemCount())
            this.quantity = ItemCount();

        inputQuantity.text = this.quantity.ToString();
    }

    public void IncreaseQuantity()
    {
        SetQuantity(++quantity);
    }

    public void DecreaseQuantity()
    {
        SetQuantity(--quantity);
    }

    public void InputQuantityByText()
    {
        int textQuantity;
        if (int.TryParse(inputQuantity.text, out textQuantity))
        {
            SetQuantity(textQuantity);
        }
    }

    int ItemCount()
    {
        if (transactionBoard.item == null)
            return 0;
        switch (transactionBoard.transactionMode)
        {
            case ShopUIManager.TransactionType.SELL:
                return PlayerInventory.inventory.GetItemCount(transactionBoard.item);
            case ShopUIManager.TransactionType.BUY:
                return ShopInventory.inventory.GetItemCount(transactionBoard.item);
            default:
                return 0;
        }
    }
}
