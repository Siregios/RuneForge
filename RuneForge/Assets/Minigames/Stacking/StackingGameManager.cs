using UnityEngine;
using System.Collections;

public class StackingGameManager : MonoBehaviour
{

    public float leftSpawnBound = -7.5f;
    public float rightSpawnBound = 7.5f;
    public float spawnStartY = 6.0f;

    public float minSpawnTime = 3.0f;
    public float maxSpawnTime = 10.0f;
    private float nextSpawnTime;
    private float spawnTimer;

    public GameObject refStackObj;

    public Sprite[] stackSprites;

	// Use this for initialization
	void Start ()
    {
        spawnStackable();
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnStackable();
            spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
            spawnStackable();
	}

    void spawnStackable()
    {
        GameObject newStack = (GameObject)Instantiate(refStackObj, new Vector2(Random.Range(leftSpawnBound, rightSpawnBound), spawnStartY), Quaternion.identity);
        newStack.GetComponent<SpriteRenderer>().sprite = stackSprites[(int)Random.Range(0, stackSprites.Length)];
        newStack.GetComponent<StackableBehvior>().Drop();
    }
}
