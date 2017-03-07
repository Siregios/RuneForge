using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour {
    Item selectedItem;

    public InventoryPage inventoryPage;
    public ItemListUI inventoryItemList;

    void Awake()
    {
        inventoryItemList.AddButtonFunction(ClickInventoryItemButton);
    }

    void ClickInventoryItemButton(Item item)
    {
        inventoryPage.SetItem(item);
    }
}
