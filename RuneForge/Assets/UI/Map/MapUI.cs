using UnityEngine;
using System.Collections;

public class MapUI : MonoBehaviour {

    private bool isShowing = false;
    public GameObject MapPanel;
    private GameObject currentLocation;


	void Start () {      
        foreach(string location in LocationManager.locationList[0].connections.Keys)
        {
            Debug.Log(location);
        }

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
