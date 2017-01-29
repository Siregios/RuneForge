using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SortGameManager : MonoBehaviour {

    public AudioManager AudioManager;
    private AudioSource music;
    bool musicOn = false;
    //Draggable runes and bubble spawns
    public GameObject[] runes;
    public GameObject[] bubbles;
    public GameObject[] characters;
    public int charY = -7;

    //Check if object is clicked on and allow drag
    bool grabbed = false;
    GameObject drag;

    //How many are currently spawned
    public int currentSpawn = 0;

    //resets drag position
    Vector3 oldPos;
    
    //Intervals to how fast bubbles will spawn    
    float timeToSpawn = 1.5f;
    float time;
    public List<int> WAKEMEUPINSIDE = new List<int>();
    [HideInInspector]
    public List<int> bubbleSpawn = new List<int>();
    [HideInInspector]
    public List<int> characterSpawn = new List<int>();
    bool again = false;
    bool againSame = false;
    bool loopOnce = false;
    bool running = false;

    public Timer timer;
    public Score score;

    void Awake()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        music = this.gameObject.GetComponent<AudioSource>();
    }

    void Start () {
        time = 0;
        for (int i = 0; i < 3; i++)
        {
            bubbleSpawn.Add(i);
            characterSpawn.Add(i);
        }
    }
		
	void Update () {
        if (!musicOn)
        {
            music.Play();
            musicOn = true;
        }

        if (timer.time <= 20)        
            timeToSpawn = 2f;
        if (timer.time <= 10)
            timeToSpawn = 2.25f;

        if (timer.timeEnd)
        {
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        }

        //Check if any characters are requesting an item.
        if (currentSpawn < 3 && time <= 0 && !running)
        {
            running = true;
            spawnTarget();
        }

        //Otherwise subtract timer while we still needa spawn more stuff OKAY EFREN JESUS I WAS JUST TRYING TO EXPLAIN THIS PART TO YOU OKAY?
        else if (currentSpawn < 3 && time > 0)
        {
            time -= Time.deltaTime;
        }

        else if (currentSpawn == 3 && time <= 0)
        {
            time = timeToSpawn;
        }


        //On click, check if you clicked on rune by tag and if so then grab it.
	    if (Input.GetMouseButtonDown(0))
        {
            checkClickedObject();
        }

        //While left click down, drag object around.
        if (Input.GetMouseButton(0))
        {
            dragObject();        
        }

        //On left click let go, object is donezoes.
        if (Input.GetMouseButtonUp(0))
        {
            resetPosition();
        }
	}

    //Functions for dragging objects

    void checkClickedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit != false && hit.collider != null)
        {
            if (hit.collider.tag == "Red" || hit.collider.tag == "Yellow" || hit.collider.tag == "Blue" || hit.collider.tag == "Green")
            {
                if (hit.collider.gameObject.transform.parent.name == "ItemSet")
                {
                    grabbed = true;
                    drag = hit.collider.gameObject;
                    oldPos = hit.collider.gameObject.transform.position;
                }
            }

        }
        //Makes cursor invisible
        if (grabbed)
            Cursor.visible = false;
    }

    void dragObject()
    {
        if (grabbed)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            drag.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0f);
        }
    }

    public void resetPosition()
    {
        if (grabbed)
        {
            drag.transform.position = oldPos;
            drag = null;
            Cursor.visible = true;
            grabbed = false;
        }
    }

    public void spawnTarget()
    {        
        if (bubbleSpawn.Count > 0 && currentSpawn < 3)
        {
            int randomInt = bubbleSpawn[Random.Range(0, bubbleSpawn.Count)];
            bubbles[randomInt].SetActive(true);
            bubbleSpawn.Remove(randomInt);
            WAKEMEUPINSIDE.Add(randomInt);

            //Sets invisible
            Color tmp = bubbles[randomInt].GetComponent<SpriteRenderer>().color;
            tmp.a = 0f;
            bubbles[randomInt].GetComponent<SpriteRenderer>().color = tmp;

            //Spawns rune
            time = timeToSpawn;
            int randomRune = Random.Range(0, 4);
            GameObject spawnedRune = (GameObject)Instantiate(runes[randomRune], bubbles[randomInt].transform.position, Quaternion.identity);
            spawnedRune.transform.parent = bubbles[randomInt].transform;
            spawnedRune.SetActive(false);                

            //Placeholder to fit the object in bubble
            spawnedRune.transform.position = new Vector3(spawnedRune.transform.position.x, spawnedRune.transform.position.y + 0.5f, spawnedRune.transform.position.z);
            spawnCharacter(randomInt);
            if (timer.time <= 20 && timer.time > 10 && !loopOnce)
            {
                again = true;
                loopOnce = true;
            }
            else if (timer.time <= 10 && !loopOnce)
            {
                again = true;
                againSame = true;
                loopOnce = true;
            }
        }
        else
        {
            again = false;
            againSame = false;
            running = false;
            loopOnce = false;
        }
    }

    void spawnCharacter(int i)
    {
        if (characterSpawn.Count > 0 && currentSpawn < 3)
        {
            int randomChar = characterSpawn[Random.Range(0, characterSpawn.Count)];
            if (characters[randomChar].transform.position.y == charY)
            {
                characterSpawn.Remove(randomChar);
                WAKEMEUPINSIDE.Add(randomChar);
                currentSpawn++;
                characters[randomChar].transform.position = new Vector3(bubbles[i].transform.position.x, charY, 0);
                characters[randomChar].GetComponent<SortMove>().moveUp = true;
                characters[randomChar].transform.parent = bubbles[i].transform;
                if (again)
                {
                    spawnTarget();
                    again = false;
                }
                else if (againSame)
                {
                    spawnTarget();
                    againSame = false;
                }
            }
        }
        if (!again && !againSame)
        {
            running = false;
            loopOnce = false;
        }
    }
}
