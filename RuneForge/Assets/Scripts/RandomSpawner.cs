using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour {
    //public bool thereCanOnlyBeOne = false;
    //public float spawnInterval = 1f;
    public GameObject objectToSpawn;

    //multiple objects to spawn RNG god pls
    public GameObject[] multipleObjectSpawn;
    public bool multiple = false;

    //spawn bounds
    public bool bounds;
    public Vector3 minBoundPos;
    public Vector3 maxBoundPos;

    [HideInInspector]
    public GameObject previousObject = null;
    Vector3 worldBounds;
    //float cooldown;
    int pad = 1;

    void Start()
    {
        //cooldown = spawnInterval;
        worldBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void Update()
    {
        //cooldown -= Time.deltaTime;

        //if (cooldown <= 0)
        //{
        //    SpawnObject();
        //}
    }

    public GameObject SpawnObject()
    {
        //if (thereCanOnlyBeOne)
        //    Destroy(previousObject);
        Vector3 randomPos;
        if (bounds)
        {
            randomPos = new Vector3(Random.Range(minBoundPos.x + pad, maxBoundPos.x - pad),
                                Random.Range(minBoundPos.y + pad, maxBoundPos.y - pad), 0);
        }
        else
        {
            randomPos = new Vector3(Random.Range(-worldBounds.x + pad, worldBounds.x - pad),
                                    Random.Range(-worldBounds.y + pad, worldBounds.y - pad), 0);
        }
        GameObject spawnedObject;
        if (multiple)
                spawnedObject = Instantiate(multipleObjectSpawn[Random.Range(0,multipleObjectSpawn.Length)], randomPos, Quaternion.identity) as GameObject;
        else        
                spawnedObject = Instantiate(objectToSpawn, randomPos, Quaternion.identity) as GameObject;
        
        previousObject = spawnedObject;
        return spawnedObject;
        //cooldown = spawnInterval;
    }
}