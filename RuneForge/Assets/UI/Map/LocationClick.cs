using UnityEngine;
using System.Collections;

public class LocationClick : MonoBehaviour {
    GameObject mapUI;
    MapUI mapScript;

    void Start()
    {
        mapUI = GameObject.Find("MapUI");
        mapScript = mapUI.GetComponent<MapUI>();
    }

	public void LocationSelect()
    {
        foreach (string name in LocationManager.locationDict.Keys)
            if (name == mapScript.currentLocation.name)
            {
                foreach (string location in LocationManager.locationDict[name].connections.Keys)
                {
                    if (this.gameObject.name == location)
                    {
                        mapScript.currentLocation = this.gameObject;
                        mapScript.resetLines();
                        mapScript.setLines();
                        //mapScript.inAir = true;
                    }                    
                }
            }
    }
}   
