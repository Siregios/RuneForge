using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HookManager : MonoBehaviour {

    RandomSpawner spawner;
    public float spawnInterval = 2f;
    public int maxObjects = 10;
    [HideInInspector]
    public int currentObjects = 0;
    float cooldown;

    public Text textRemaining;
    public Text scoreText;
    public int remainingHooks = 5;
    [HideInInspector]
    public bool endGame = false;

    [HideInInspector]
    public int score = 0;

    [HideInInspector]
    public int neg = 0;


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
        textRemaining.text = "Hooks Remaining: " + remainingHooks.ToString();
        cooldown -= Time.deltaTime;
        scoreText.text = "Score: " + score.ToString();

        if (cooldown <= 0 && currentObjects < maxObjects)
        {
            SpawnObject();
            currentObjects++;
        }

        if (endGame)
        {
            //Will implement result screen later
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
            //MasterGameManager.instance.sceneManager.LoadScene("Workshop");
        }
    }

    public void SpawnObject()
    {
        cooldown = spawnInterval;
        //This makes less mei's spawn.    
        GameObject instantiatedObj = spawner.SpawnObject();        
        if (instantiatedObj.name == "mei(Clone)")
        {
            if (neg >= 3)
            {                
                Destroy(instantiatedObj);
                currentObjects--;
            }
            else
            {
                neg++;
            }

        }
    }


}

