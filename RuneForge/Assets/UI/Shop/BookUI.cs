using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Tab
{
    Materials,
    Runes,
    YourStock,
    ShopStock
}

public class BookUI : MonoBehaviour
{
    [HideInInspector]
    public TransactionUI transactionPage;
    [HideInInspector]
    public ItemListUI itemList;


    void Awake()
    {
        transactionPage = GameObject.Find("TransactionPanel").GetComponent<TransactionUI>();
        itemList = GameObject.Find("ItemsPanel").GetComponent<ItemListUI>();
    }

    public void SelectItem(Item item)
    {
        transactionPage.LoadItem(item);
    }
}
