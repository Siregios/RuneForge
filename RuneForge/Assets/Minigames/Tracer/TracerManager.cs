using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracerManager : MonoBehaviour {

    RandomSpawner spawner;

    public GameObject lineRenderer = null;
    public GameObject trailRenderer = null;
    public float spawnInterval = 1f;
    public GameObject map;
    int count = 0;

    [HideInInspector]
    public int score = 0;

    GameObject currentTrail = null;
    float cooldown;

    List<GameObject> lineList = new List<GameObject>();
    //Vector3 previousDotLoc = Vector3.back;

    //For testing purposes only
    public bool trailStays = false;

    void Awake()
    {
        spawner = this.GetComponent<RandomSpawner>();
        Cursor.visible = false;
    }

    void Start()
    {
        CreateNewTrail();
        cooldown = spawnInterval;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            //SpawnDot();
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentTrail.transform.position = mousePos;        
    }

    public void DotTouched(GameObject dot)
    {
        score++;
        if (trailStays)
            DropTrail();
        currentTrail.GetComponent<TrailRenderer>().time += .01f;

        //Vector3 newLoc = dot.transform.position;
        
        ///Straight lines between 2 points below

        //if (previousDotLoc != Vector3.back)
        //{
        //    GameObject newLine = Instantiate(lineRenderer) as GameObject;
        //    LineRenderer line = newLine.GetComponent<LineRenderer>();
        //    line.SetPosition(0, previousDotLoc);
        //    line.SetPosition(1, newLoc);
        //    line.sortingLayerName = "Background";
        //    lineList.Add(newLine);
        //}
        //else
        //{
        //    GameObject newLine = Instantiate(lineRenderer) as GameObject;
        //    LineRenderer line = newLine.GetComponent<LineRenderer>();
        //    line.SetPosition(0, newLoc - new Vector3(.1f, .1f, 0));
        //    line.SetPosition(1, newLoc + new Vector3(.1f, .1f, 0));
        //    lineList.Add(newLine);
        //}

        //previousDotLoc = newLoc;
        SpawnDot(dot);
    }

    public void DotMissed(GameObject dot)
    {
        //score = 0;
        currentTrail.GetComponent<TrailRenderer>().time = .25f;
        foreach (GameObject line in lineList){
            Destroy(line);
        }
        lineList.Clear();
        SpawnDot(dot);
        //previousDotLoc = Vector3.back;
    }

    public void SpawnDot(GameObject dot)
    {
        //spawner.SpawnObject();
        if (dot.GetComponent<Mapper>().next != null)
        {
            dot.GetComponent<Mapper>().next.SetActive(true);
        }
        else {
            if (count < 5)
            {
                int temp = (int)Mathf.Round(Random.value * 4);
                Debug.Log(temp);
                Destroy(GameObject.FindGameObjectWithTag("TraceMap"));
                map = (GameObject)Instantiate(Resources.Load("TraceMaps/Map" + temp));
                count++;
            }
            else
            {
                Destroy(GameObject.FindGameObjectWithTag("TraceMap"));
            }
        }
        cooldown = spawnInterval;
    }

    void CreateNewTrail()
    {
        currentTrail = (GameObject)Instantiate(trailRenderer);
    }

    void DropTrail()
    {
        TrailRenderer trailRend = currentTrail.GetComponent<TrailRenderer>();
        trailRend.time = 5;
        trailRend.material.SetColor("_TintColor", Color.cyan);
        trailRend.autodestruct = true;
        CreateNewTrail();
    }
}
