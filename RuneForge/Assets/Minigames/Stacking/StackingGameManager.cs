using UnityEngine;
using System.Collections;

public class StackingGameManager : MonoBehaviour
{
    public Timer timer;
    public Score score;
    public float leftSpawnBound = -7.5f;
    public float rightSpawnBound = 7.5f;
    public float spawnStartY = 6.0f;

    public float minSpawnTime = 3.0f;
    public float maxSpawnTime = 10.0f;
    private float nextSpawnTime;
    private float spawnTimer;

    public GameObject refStackObj;

    public Sprite[] stackSprites;

    public int spawnNum;
    public int stackSize;

	// Use this for initialization
	void Start ()
    {
        spawnNum = 0;
        stackSize = 0;
        spawnStackable();
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        score.s = stackSize * 100;
        if (timer.timeEnd)
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
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
        newStack.name = string.Format("Stackable {0:0}", spawnNum++);
        newStack.GetComponent<SpriteRenderer>().sprite = stackSprites[(int)Random.Range(0, stackSprites.Length)];
        newStack.GetComponent<StackableBehavior>().Drop();
    }
}
