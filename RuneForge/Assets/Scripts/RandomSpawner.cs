using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour {
    //public bool thereCanOnlyBeOne = false;
    //public float spawnInterval = 1f;
    public GameObject objectToSpawn;

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
        if (bounds)
        {
            worldBounds = new Vector3(Mathf.Clamp(Screen.width, minBoundPos.x, maxBoundPos.x), Mathf.Clamp(Screen.height, minBoundPos.y, maxBoundPos.y), 0);
        }
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
        randomPos = new Vector3(Random.Range(-worldBounds.x + pad, worldBounds.x - pad),
                                Random.Range(-worldBounds.y + pad, worldBounds.y - pad), 0);
        GameObject spawnedObject = Instantiate(objectToSpawn, randomPos, Quaternion.identity) as GameObject;
        previousObject = spawnedObject;
        //cooldown = spawnInterval;

        return spawnedObject;
    }
}