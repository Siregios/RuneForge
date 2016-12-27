using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    public Customer customer;

    void Start()
    {
        //For testing
        SpawnCustomer(MasterGameManager.instance.questGenerator.todaysQuests[0]);
    }

    public void SpawnCustomer(Quest quest)
    {
        GameObject newCustomer = Instantiate(customer.gameObject, this.transform);
        Customer newCustomerScript = newCustomer.GetComponent<Customer>();
        newCustomerScript.SetItem(quest);
    }
}
