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
        //CreateWorkOrder(ItemCollection.itemDict["Fire Rune"], false, false);
        //WorkOnOrder(workorderList[0]);
        //CreateWorkOrder(ItemCollection.itemDict["Water Rune"], true);
    }

    public void CreateWorkOrder(Item item, bool isEnhanced, bool isRandom)
    {
        if (!IsFull())
        {
            workorderList.Add(new WorkOrder(item, workorderList.Count + 1, isEnhanced, isRandom));
        }
    }

    public void CreateWorkOrderTutorial(Item item)
    {
        workorderList.Add(new WorkOrder(item, workorderList.Count + 1, false, false));
        workorderList[workorderList.Count - 1].requiredStages = 1;
    }

    public bool IsFull()
    {
        return workorderList.Count >= maxWorkOrders;
    }

    public Item CompleteOrder(WorkOrder order)
    {
        PlayerInventory.inventory.AddItem(order.item);
        workorderList.RemoveAt(order.orderNumber - 1);
        for (int i = 0; i < workorderList.Count; i++)
        {
            workorderList[i].orderNumber = i + 1;
        }

        return order.item;
    }

    public void WorkOnOrder(WorkOrder order)
    {
        if (currentWorkOrders.Contains(order))
        {
            Debug.LogWarningFormat("Order #{0}) {1} already exists in currentWorkOrders", order.orderNumber, order.item.name);
            return;
        }
        currentWorkOrders.Add(order);
    }

    public void CancelWorkOnOrder(WorkOrder order)
    {
        if (currentWorkOrders.Contains(order))
        {
            currentWorkOrders.Remove(order);
        }
    }
}
