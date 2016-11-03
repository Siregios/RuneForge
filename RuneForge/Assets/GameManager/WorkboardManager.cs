using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkboardManager : MonoBehaviour {
    public int maxWorkOrders = 5;
    public List<WorkOrder> workorderList = new List<WorkOrder>();

    void Awake()
    {
        //For testing
        CreateWorkOrder(ItemCollection.itemDict["Fire_Rune"], false);
        CreateWorkOrder(ItemCollection.itemDict["Water_Rune"], true);
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
        workorderList.RemoveAt(order.orderNumber - 1);
        for (int i = 0; i < workorderList.Count; i++)
        {
            workorderList[i].orderNumber = i + 1;
        }
    }
}
