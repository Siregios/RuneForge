using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SortGameManager : MonoBehaviour {

    private AudioManager AudioManager;
    private AudioSource music;

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
    float timeToSpawn = 1f;
    float time;

    public Timer timer;
    public Score score;

    void Awake()
    {
        //AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        music = this.gameObject.GetComponent<AudioSource>();
    }

    void Start () {
        time = 0;
        music.Play();
    }
		
	void Update () {

        if (timer.timeEnd)
        {
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        }
        //Check if any characters are requesting an item.
        if (currentSpawn < 3 && time <= 0)
        {
            spawnTarget();
        }

        //Otherwise subtract timer while we still needa spawn more stuff OKAY EFREN JESUS I WAS JUST TRYING TO EXPLAIN THIS PART TO YOU OKAY?
        if (currentSpawn < 3)
        {
            time -= Time.deltaTime;
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
        int randomInt = 0;
        bool check = true;
        while (check)
        {        
            randomInt = Random.Range(0, 3);
            if (bubbles[randomInt].activeSelf == false)
            {
                check = false;
                bubbles[randomInt].SetActive(true);
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
                currentSpawn++;

                //Placeholder to fit the object in bubble
                spawnedRune.transform.position = new Vector3(spawnedRune.transform.position.x, spawnedRune.transform.position.y + 0.5f, spawnedRune.transform.position.z);
            }
        }
        spawnCharacter(randomInt);        
    }

    void spawnCharacter(int i)
    {
        bool check = true;
        while (check)
        {
            int randomChar = Random.Range(0, 3);
            if (characters[randomChar].transform.position.y == charY)
            {
                check = false;
                characters[randomChar].transform.position = new Vector3(bubbles[i].transform.position.x, charY, 0);
                characters[randomChar].GetComponent<SortMove>().moveUp = true;
                characters[randomChar].transform.parent = bubbles[i].transform;
            }
        }
    }
}
