using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracerManager : MonoBehaviour {

    RandomSpawner spawner;

    public GameObject lineRenderer = null;
    public float spawnInterval = 1f;
    float cooldown;


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
        }
        previousDotLoc = newLoc;
        SpawnDot();
    }

    public void SpawnDot()
    {
        GameObject newDot = spawner.SpawnObject() as GameObject;
        //DotController dot = newDot.GetComponent<DotController>();
        
        cooldown = spawnInterval;
    }
}
