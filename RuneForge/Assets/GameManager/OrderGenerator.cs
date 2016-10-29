using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrderGenerator : MonoBehaviour {
    public List<Order> todaysOrders = new List<Order>();

    void Awake()
    {
        GenerateOrders();
    }

    public void GenerateOrders()
    {
        todaysOrders.Clear();
        for (int i = 0; i < MasterGameManager.instance.actionClock.Day; i++)
        {
            todaysOrders.Add(new Order());
        }
    }
}
