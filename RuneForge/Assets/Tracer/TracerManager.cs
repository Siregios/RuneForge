using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracerManager : MonoBehaviour {

    RandomSpawner spawner;

    public GameObject lineRenderer = null;
    public float spawnInterval = 1f;
    float cooldown;

    List<GameObject> lineList = new List<GameObject>();
    Vector3 previousDotLoc = Vector3.back;

    void Awake()
    {
        spawner = this.GetComponent<RandomSpawner>();
    }

    void Start()
    {
        cooldown = spawnInterval;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            SpawnDot();
        }
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
            line.sortingLayerName = "Background";
            lineList.Add(newLine);
        }
        //else
        //{
        //    GameObject newLine = Instantiate(lineRenderer) as GameObject;
        //    LineRenderer line = newLine.GetComponent<LineRenderer>();
        //    line.SetPosition(0, newLoc - new Vector3(.1f, .1f, 0));
        //    line.SetPosition(1, newLoc + new Vector3(.1f, .1f, 0));
        //    lineList.Add(newLine);
        //}
        previousDotLoc = newLoc;
        SpawnDot();
    }

    public void DotMissed(GameObject dot)
    {
        Vector3 newLoc = dot.transform.position;
        foreach (GameObject line in lineList){
            Destroy(line);
        }
        lineList.Clear();
        previousDotLoc = Vector3.back;
    }

    public void SpawnDot()
    {
        GameObject newDot = spawner.SpawnObject() as GameObject;
        cooldown = spawnInterval;
    }
}
