using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracerManager : MonoBehaviour {
    public GameObject lineRenderer = null;
    public GameObject trailRenderer = null;
    //Number of maps that exist in Assets/TraceMaps
    public int traceMapCount = 4;
    //Number of maps to spawn in one playthrough
    public int mapsPerPlay = 5;
    public float spawnInterval = 1f;
    int count = 1;

    [HideInInspector]
    public int score = 0;

    GameObject currentTrail = null;

    void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
        CreateNewTrail();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentTrail.transform.position = mousePos;        
    }

    public void DotTouched(GameObject dot)
    {
        score++;
        
        currentTrail.GetComponent<TrailRenderer>().time += .01f;
        
        SpawnDot(dot);
    }

    public void DotMissed(GameObject dot)
    {
        SpawnDot(dot);
    }

    public void SpawnDot(GameObject dot)
    {
        if (dot.GetComponent<Mapper>().next != null)
        {
            dot.GetComponent<Mapper>().next.SetActive(true);
        }
        else {
            if (count < mapsPerPlay)
            {
                int randomMapNumber = Random.Range(1, traceMapCount + 1);
                Debug.LogFormat("Spawning Map{0}", randomMapNumber);
                Destroy(GameObject.FindGameObjectWithTag("TraceMap"));
                Instantiate(Resources.Load("TraceMaps/Map" + randomMapNumber));
                count++;
            }
            else
            {
                Destroy(GameObject.FindGameObjectWithTag("TraceMap"));
                currentTrail.SetActive(false);
                Cursor.visible = true;
                //Should show results screen here first.
                MasterGameManager.instance.sceneManager.LoadScene("Workshop");
            }
        }
    }

    void CreateNewTrail()
    {
        currentTrail = (GameObject)Instantiate(trailRenderer);
    }
}
