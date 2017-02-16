using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    public Customer customer;
    int index = 0;
    void Start()
    {
        //For testing
        SpawnCustomer(MasterGameManager.instance.questGenerator.todaysQuests[Random.Range(0, MasterGameManager.instance.questGenerator.todaysQuests.Count)]);
    }

    public void SpawnCustomer(Quest quest)
    {
        GameObject newCustomer = Instantiate(customer.gameObject, this.transform);
        Customer newCustomerScript = newCustomer.GetComponent<Customer>();
        newCustomerScript.SetItem(quest);
    }
}
