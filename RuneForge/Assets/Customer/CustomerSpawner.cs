using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    public List<Customer> customerList;
    int index = 0;
    void Start()
    {
        //For testing
        SpawnCustomer(MasterGameManager.instance.questGenerator.todaysQuests[Random.Range(0, MasterGameManager.instance.questGenerator.todaysQuests.Count)]);
    }

    public void SpawnCustomer(Quest quest)
    {
        Customer randomCustomer = customerList[Random.Range(0, customerList.Count)];
        GameObject newCustomer = Instantiate(randomCustomer.gameObject, this.transform);
        Customer newCustomerScript = newCustomer.GetComponent<Customer>();
        newCustomerScript.SetItem(quest);
    }
}
