using UnityEngine;
using System.Collections;

public class NoteTracker : MonoBehaviour {
    public int accuracy = 0;
    public float speed = 3.5f;
    public RandomSpawnNote scriptNote;
    public GameObject center;
    public bool canDie = false;
    bool trigW, trigS, trigA, trigD, switchOver, ws, ad = false;

    //ALL FOR DOUBLE NOTES
    public Sprite[] doubleSprite;
    float startSlerp;
    float timerDouble = 0.1f;
    SpriteRenderer noteSprite;


	void Start () {
        noteSprite = gameObject.GetComponent<SpriteRenderer>();
        scriptNote = GameObject.Find("RandomSpawn").GetComponent<RandomSpawnNote>();
        center = GameObject.Find("center");
	}
	
	
	void Update () {

        //Checks if the note is in front of the list
        if (gameObject == scriptNote.keyNotes[0])
        {
            canDie = true;
        }

        //Checks for double note
        if (switchOver)
        {
            if (ws && noteSprite.sprite == doubleSprite[1])
            {
                GameObject aKey = GameObject.Find("a_key");
                doubleNoteEvent(aKey, KeyCode.A);
            }
            else if (ws && noteSprite.sprite == doubleSprite[0])
            {
                GameObject aKey = GameObject.Find("d_key");
                doubleNoteEvent(aKey, KeyCode.D);
            }
            else if (ad && noteSprite.sprite == doubleSprite[0])
            {
                GameObject wKey = GameObject.Find("w_key");
                doubleNoteEvent(wKey, KeyCode.W);
            }
            else if (ad && noteSprite.sprite == doubleSprite[1])
            {
                GameObject sKey = GameObject.Find("s_key");
                doubleNoteEvent(sKey, KeyCode.S);
            }
        }

        //Checks for input when note is on hitbox
        if (Input.GetKeyDown(KeyCode.W) && trigW && !switchOver)
        {
            checkAccuracy();
            DoubleSpawn();
            if (!switchOver)
                Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S) && trigS && !switchOver)
        {
            checkAccuracy();
            DoubleSpawn();
            if (!switchOver)
                Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.A) && trigA && !switchOver)
        {
            checkAccuracy();
            DoubleSpawn();
            if (!switchOver)
                Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.D) && trigD && !switchOver)
        {
            checkAccuracy();
            DoubleSpawn();
            if (!switchOver)
                Destroy(gameObject);
        }

        //Move towards location
        if (!switchOver)
            transform.position = Vector3.MoveTowards(transform.position, center.transform.position, speed * Time.deltaTime);
        
    }            

    void OnTriggerEnter2D(Collider2D other)
    {
        //Sets up when the note can be dieded
        if (other.gameObject.name == "hitbox")
            if (other.transform.parent.name == "w_key" && canDie)
                trigW = true;
            if (other.transform.parent.name == "s_key" && canDie)
                trigS = true;
            if (other.transform.parent.name == "a_key" && canDie)
                trigA = true;
            if (other.transform.parent.name == "d_key" && canDie)
                trigD = true;

        //Timing is key
        if (other.gameObject.name != "hitbox")
            accuracy++;

        //Otherwise you miss
        if (accuracy == 4)
        {
            scriptNote.miss++;
            Destroy(gameObject);
            scriptNote.score += -5;
            scriptNote.hitText.text = "Miss!";
        }        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Once key leaves, you can no longer press (Probably not needed but added for safety)
        trigW = false;
        trigS = false;
        trigA = false;
        trigD = false;
    }

    //Checks the score and allocates the amount while updating the UI
    void checkAccuracy()
    {
        if (accuracy == 0)
        {
            scriptNote.hitText.text = "Good?";
            scriptNote.good++;
            scriptNote.score++;
        }
        else if (accuracy == 1 || accuracy == 3)
        {
            scriptNote.hitText.text = "Great.";
            scriptNote.great++;
            scriptNote.score += 2;
        }
        else if (accuracy == 2)
        {
            scriptNote.hitText.text = "Perfect!";
            scriptNote.perfect++;
            scriptNote.score += 4;
        }
    }

    //When destroyed, removes itself from the front of the list.
    void OnDestroy()
    {
        scriptNote.keyNotes.Remove(gameObject);
    }

    //Will spawn a double note if it is a double.
    bool DoubleSpawn()
    {
        if (gameObject.name == "w_double(Clone)" || gameObject.name == "s_double(Clone)")
        {
            ws = true;
            if (gameObject.name == "w_double(Clone)")
                noteSprite.sprite = doubleSprite[0];
            else
                noteSprite.sprite = doubleSprite[1];

        }
        else if (gameObject.name == "a_double(Clone)" || gameObject.name == "d_double(Clone)")
        {
            ad = true;
            if (gameObject.name == "a_double(Clone)")
                noteSprite.sprite = doubleSprite[0];
            else
                noteSprite.sprite = doubleSprite[1];
        }
        else
        {
            return false;
        }
        switchOver = true;                
        accuracy = -999;
        startSlerp = Time.time;
        
        return switchOver;
    }

    //Handles the events for a double note.
    void doubleNoteEvent(GameObject keyLoc, KeyCode input)
    {
        //if w or s key
        if (ws)
        {
            if (Mathf.Abs(transform.position.x - keyLoc.transform.position.x) < 0.003f)
            {
                transform.position = keyLoc.transform.position;
                accuracy = 3;
                timerDouble -= Time.deltaTime;
                if (timerDouble <= 0)
                {
                    scriptNote.miss++;
                    Destroy(gameObject);
                    scriptNote.score += -5;
                    scriptNote.hitText.text = "Miss!";
                }
            }
            else            
                transform.position = Vector3.Slerp(transform.position, keyLoc.transform.position, (Time.time - startSlerp) / speed * 2);
            if (transform.position.y <= 0.3f)
            {
                if (Input.GetKeyDown(input))
                {
                    accuracy = 2;
                    checkAccuracy();
                    Destroy(gameObject);
                }
            }
        }
        else if (ad)
        {
            if (Mathf.Abs(transform.position.y - keyLoc.transform.position.y) < 0.003f)
            {
                transform.position = keyLoc.transform.position;
                accuracy = 3;
                timerDouble -= Time.deltaTime;
                if (timerDouble <= 0)
                {
                    scriptNote.miss++;
                    Destroy(gameObject);
                    scriptNote.score += -5;
                    scriptNote.hitText.text = "Miss!";
                }
            }
            else
                transform.position = Vector3.Slerp(transform.position, keyLoc.transform.position, (Time.time - startSlerp) / speed * 2);
            if (transform.position.x <= 0.3f)
            {
                if (Input.GetKeyDown(input))
                {
                    accuracy = 2;
                    checkAccuracy();
                    Destroy(gameObject);
                }
            }
        }
    }
}
