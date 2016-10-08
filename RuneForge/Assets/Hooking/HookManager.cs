using UnityEngine;
using System.Collections;

public class HookManager : MonoBehaviour {

    RandomSpawner spawner;
    public float spawnInterval = 2f;
    public int maxObjects = 10;
    public int currentObjects = 0;
    float cooldown;    
    Vector3 previousDotLoc = Vector3.back;


    void Awake ()
    {
        spawner = this.GetComponent<RandomSpawner>();
    }

    void Start ()
    {
        cooldown = spawnInterval;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0 && currentObjects < maxObjects)
        {
            SpawnObject();
            currentObjects++;
        }
    }

    public void SpawnObject()
    {
        GameObject target = spawner.SpawnObject() as GameObject;
        cooldown = spawnInterval;
    }
}

