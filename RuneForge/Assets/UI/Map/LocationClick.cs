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
        mapScript.currentLocation = this.gameObject;
        mapScript.inAir = true;
    }
}   
