using UnityEngine;
using System.Collections;

public class MapUI : MonoBehaviour {

	// Use this for initialization
	void Start () {

        foreach(string location in LocationManager.locationList[0].connections.Keys)
        {
            Debug.Log(location);
        }

    }
	
	// Update is called once per frame
	void Update () {


	
	}
}
