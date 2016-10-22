using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapUI : MonoBehaviour {

    private bool isShowing = false;
    public GameObject MapPanel;
    public GameObject currentLocation;
    public bool inAir = false;
    public  Image lineImage = null;
    public List<Image> lineList = new List<Image>();   

    void Start () {      
        currentLocation = GameObject.Find("Snowdin");
        setLines();      
    }

    //Debugging purposes
    //foreach (Location location in LocationManager.locationList)
    //{
    //    if (currentLocation.name == location.name)
    //    {

    //    }
    //}



    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isShowing = !isShowing;            
                MapPanel.SetActive(isShowing);
            foreach (Image line in lineList)
            {
                line.gameObject.SetActive(isShowing);
            }
        }	      
	}

    public void setLines()
    {
        foreach (string name in LocationManager.locationDict.Keys)
            if (name == currentLocation.name)
                foreach (string location in LocationManager.locationDict[name].connections.Keys)
                {
                    Vector3 difference = GameObject.Find(location).transform.position - currentLocation.transform.position;
                    Image newLine = Instantiate(lineImage) as Image;
                    newLine.gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
                    newLine.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(difference.magnitude, 1);
                    newLine.gameObject.transform.position = currentLocation.transform.position;
                    float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    newLine.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                    lineList.Add(newLine);
                }
        setPlayer();
    }

    public void setPlayer()
    {
        GameObject player = GameObject.Find("player");
        player.transform.position = currentLocation.transform.position;
    }

    public void resetLines()
    {
        foreach (Image line in lineList)
        {            
            Destroy(line.gameObject);
        }
        lineList.Clear();
    }
}

