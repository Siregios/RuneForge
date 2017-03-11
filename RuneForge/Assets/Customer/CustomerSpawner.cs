﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    public List<Customer> customerList;
    public static int currentDay = 0;
    public List<Transform> spawnPositions;
    //public int questIndex = 0;
    void Start()
    {
        //SpawnCustomer(MasterGameManager.instance.questGenerator.todaysQuests[Random.Range(0, MasterGameManager.instance.questGenerator.todaysQuests.Count)]);
        //SpawnCustomer(MasterGameManager.instance.questGenerator.todaysQuests[questIndex]);
        if (currentDay != MasterGameManager.instance.actionClock.Day)
        {
            NewDayCustomers();
            currentDay = MasterGameManager.instance.actionClock.Day;
        }
    }

    /// <summary>
    /// Spawns all the customers for today
    /// </summary>
    public void NewDayCustomers()
    {
        int pos = 0;
        foreach (Quest quest in MasterGameManager.instance.questGenerator.todaysQuests)
        {
            SpawnCustomer(quest, pos);
            pos++;
        }

        if (pos == 0)
            Debug.Log("No quests today");
    }

    public void SpawnCustomer(Quest quest, int posIndex)
    {
        Customer randomCustomer = customerList[Random.Range(0, customerList.Count)];
        GameObject newCustomer = Instantiate(randomCustomer.gameObject, this.transform);
        newCustomer.transform.position = spawnPositions[posIndex].position;
        Customer newCustomerScript = newCustomer.GetComponent<Customer>();
        newCustomerScript.SetItem(quest);
        //questIndex++;
    }
}
