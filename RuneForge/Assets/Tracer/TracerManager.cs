using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracerManager : MonoBehaviour {

    RandomSpawner spawner;
    public GameObject lineRenderer = null;
    Vector3 previousDotLoc = Vector3.back;

    void Awake()
    {
        spawner = this.GetComponent<RandomSpawner>();
    }

    void Start()
    {
        //currentDot = spawner.SpawnObject();
    }

    public void DotTouched(GameObject dot)
    {
        Vector3 newLoc = dot.transform.position;
        if (previousDotLoc != Vector3.back)
        {
            GameObject newLine = Instantiate(lineRenderer) as GameObject;
            LineRenderer line = newLine.GetComponent<LineRenderer>();
            line.SetPosition(0, previousDotLoc);
            line.SetPosition(1, newLoc);
        }
        previousDotLoc = newLoc;
        //Debug.Log(currentDot.transform.position);
        //Debug.Log(dot.transform.position);
        //SpawnDot();
        spawner.SpawnObject();
    }


    
    public void SpawnDot()
    {
        spawner.SpawnObject();
        //currentDot = 
    }
}
