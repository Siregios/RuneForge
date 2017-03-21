using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    /// A static list of customers for today. Used for reloading old customers who haven't left yet.
    public static List<KeyValuePair<Quest, int>> todaysCustomers = new List<KeyValuePair<Quest, int>>();
    public List<Customer> possibleCustomers;
    public static int currentDay = 0;
    public List<Transform> spawnPositions;

    void Start()
    {
        for (int i = 0; i < MasterGameManager.instance.questGenerator.maxQuestsPerDay; i++)
        {
            todaysCustomers.Add(new KeyValuePair<Quest, int>(null, -1));
        }

        if (currentDay != MasterGameManager.instance.actionClock.Day)
        {
            NewDayCustomers();
            currentDay = MasterGameManager.instance.actionClock.Day;
        }
        else
        {
            SameDayCustomers();
        }
    }

    /// <summary>
    /// Spawns all the customers for today
    /// </summary>
    public void NewDayCustomers()
    {
        int customerNum = 0;
        foreach (Quest quest in MasterGameManager.instance.questGenerator.todaysQuests)
        {
            SpawnCustomer(quest, customerNum);
            customerNum++;
        }

        if (customerNum == 0)
            Debug.Log("No quests today");
    }

    /// <summary>
    /// Respawns customers that haven't left when returning to the Store scene in the same day.
    /// </summary>
    public void SameDayCustomers()
    {
        for (int customerNum = 0; customerNum < todaysCustomers.Count; customerNum++)
        {
            KeyValuePair<Quest, int> customerQuest = todaysCustomers[customerNum];
            Quest quest = customerQuest.Key;
            int customerID = customerQuest.Value;
            if (quest != null)
            {
                //Quest quest = MasterGameManager.instance.questGenerator.todaysQuests[customerNum];
                SpawnCustomer(quest, customerNum, customerID);
            }
        }
    }

    public void SpawnCustomer(Quest quest, int customerNum, int customerID=-1)
    {
        if (customerID == -1)
            customerID = Random.Range(0, possibleCustomers.Count);

        //Save the customer in todaysCustomers to load again if the player hasn't accepted or declined their quest and returns to the store
        todaysCustomers[customerNum] = new KeyValuePair<Quest, int>(quest, customerID);

        Customer customer = possibleCustomers[customerID];
        GameObject newCustomer = Instantiate(customer.gameObject, this.transform);
        newCustomer.transform.position = spawnPositions[customerNum].position;
        Customer newCustomerScript = newCustomer.GetComponent<Customer>();
        newCustomerScript.customerNum = customerNum;
        newCustomerScript.SetItem(quest);
    }

    public static void RemoveTodaysCustomer(int customerNum)
    {
        if (customerNum < todaysCustomers.Count)
            todaysCustomers[customerNum] = new KeyValuePair<Quest, int>(null, -1);
    }
}
