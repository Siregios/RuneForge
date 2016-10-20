using UnityEngine;
using System.Collections;

public class MapUI : MonoBehaviour {

    private bool isShowing = false;
    public GameObject MapPanel;
    private GameObject currentLocation;

	void Start () {      
        currentLocation = GameObject.Find("Snowdin");
        foreach (string name in LocationManager.locationDict.Keys)
            foreach (string location in LocationManager.locationDict[name].connections.Keys)
            {
                Debug.Log("Current: " + name + " and Connected location: " + location);
            }
        //foreach (Location location in LocationManager.locationList)
        //{
        //    if (currentLocation.name == location.name)
        //    {
                
        //    }
        //}

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isShowing = !isShowing;
            MapPanel.SetActive(isShowing);
        }
	
	}
}
