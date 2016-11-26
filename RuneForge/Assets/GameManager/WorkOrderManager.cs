using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkOrderManager : MonoBehaviour {
    public int maxWorkOrders = 5;
    public List<WorkOrder> workorderList = new List<WorkOrder>();
    public List<WorkOrder> currentWorkOrders = new List<WorkOrder>();   // The Work Orders that are loaded into the current minigame.

    void Awake()
    {
        ////For testing
        CreateWorkOrder(ItemCollection.itemDict["Fire Rune"], true);
        CreateWorkOrder(ItemCollection.itemDict["Water Rune"], true);
    }

    public void CreateWorkOrder(Item item, bool isRandom)
    {
        if (!IsFull())
        {
            workorderList.Add(new WorkOrder(item, workorderList.Count + 1, isRandom));
        }
    }

    public bool IsFull()
    {
        return workorderList.Count >= maxWorkOrders;
    }

    public void CompleteOrder(WorkOrder order)
    {
        PlayerInventory.inventory.AddItem(order.item);
        workorderList.RemoveAt(order.orderNumber - 1);
        for (int i = 0; i < workorderList.Count; i++)
        {
            workorderList[i].orderNumber = i + 1;
        }
    }

    public void WorkOnOrder(WorkOrder order)
    {
        if (currentWorkOrders.Contains(order))
        {
            Debug.LogWarningFormat("Order #{0}) {1} already exists in currentWorkOrders", order.orderNumber, order.item.name);
            return;
        }
        currentWorkOrders.Add(order);

        foreach (WorkOrder boo in currentWorkOrders)
        {
            Debug.LogFormat("Order #{0}) {1} being worked on.", boo.orderNumber, boo.item.name);
        }
    }
}
