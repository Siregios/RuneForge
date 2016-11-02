using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkboardManager : MonoBehaviour {
    public int maxWorkOrders = 5;
    List<WorkOrder> workorderList = new List<WorkOrder>();

    public void CreateWorkOrder(Item item, bool isRandom)
    {
        if (!IsFull())
        {
            workorderList.Add(new WorkOrder(item, workorderList.Count + 1, isRandom));
        }

        foreach (WorkOrder order in workorderList)
        {
            Debug.LogFormat("{0}) {1}: Stage {2}/{3} | Score: {4} | Random: {5}", order.orderNumber, order.item.name, order.currentStage, order.requiredStages, order.score, order.isRandom);
        }
    }

    public bool IsFull()
    {
        return workorderList.Count >= maxWorkOrders;
    }
}
