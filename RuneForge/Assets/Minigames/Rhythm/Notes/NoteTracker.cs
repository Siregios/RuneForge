﻿using UnityEngine;
using System.Collections;

public class NoteTracker : MonoBehaviour
{
    public int accuracy = 0;
    public int indexNote;
    public float speed = 3.5f;
    public RandomSpawnNote scriptNote;
    public GameObject center;
    public GameObject accShow;
    public bool canDie = false;
    bool switchOver, ws, ad = false;

    //ALL FOR DOUBLE NOTES
    public Sprite[] doubleSprite;
    //float startSlerp;
    float timerDouble = 0.2f;
    SpriteRenderer noteSprite;
    bool destroyed = false;



    void Start()
    {
        noteSprite = gameObject.GetComponent<SpriteRenderer>();
        scriptNote = GameObject.Find("RandomSpawn").GetComponent<RandomSpawnNote>();
        center = GameObject.Find("center");
        StartCoroutine(FadeIn());
    }


    void Update()
    {
        //Checks if the note is in front of the list
        if (destroyed)
        {
            scriptNote.keyNotes[indexNote].Remove(gameObject);
            StopAllCoroutines();
            GetComponent<NoteTransition>().enabled = true;
        }
        else
        {
            if (gameObject == scriptNote.keyNotes[indexNote][0])
            {
                canDie = true;
            }
            else
            {
                canDie = false;
            }

            //Checks for double note
            if (switchOver)
            {
                scriptNote.keyNotes[indexNote].Remove(gameObject);
                if (ws && noteSprite.sprite == doubleSprite[1])
                {
                    GameObject aKey = GameObject.Find("a_key");
                    indexNote = 1;
                    scriptNote.keyNotes[indexNote].Insert(0, gameObject);
                    doubleNoteEvent(aKey, KeyCode.A);
                }
                else if (ws && noteSprite.sprite == doubleSprite[0])
                {
                    GameObject dKey = GameObject.Find("d_key");
                    indexNote = 3;
                    scriptNote.keyNotes[indexNote].Insert(0, gameObject);
                    doubleNoteEvent(dKey, KeyCode.D);
                }
                else if (ad && noteSprite.sprite == doubleSprite[0])
                {
                    GameObject wKey = GameObject.Find("w_key");
                    indexNote = 0;
                    scriptNote.keyNotes[indexNote].Insert(0, gameObject);
                    doubleNoteEvent(wKey, KeyCode.W);
                }
                else if (ad && noteSprite.sprite == doubleSprite[1])
                {
                    GameObject sKey = GameObject.Find("s_key");
                    indexNote = 2;
                    scriptNote.keyNotes[indexNote].Insert(0, gameObject);
                    doubleNoteEvent(sKey, KeyCode.S);
                }
            }

            //Checks for input when note is on hitbox and key pressed
            if (Input.GetKeyDown(KeyCode.W) && indexNote == 0 && !switchOver && canDie)
            {
                checkAccuracy();
                if (accuracy != 0)
                    DoubleSpawn();
                if (!switchOver && accuracy != 0)
                    destroyed = true;
            }
            if (Input.GetKeyDown(KeyCode.S) && indexNote == 2 && !switchOver && canDie)
            {
                checkAccuracy();
                if (accuracy != 0)
                    DoubleSpawn();
                if (!switchOver && accuracy != 0)
                    destroyed = true;
            }
            if (Input.GetKeyDown(KeyCode.A) && indexNote == 1 && !switchOver && canDie)
            {
                checkAccuracy();
                if (accuracy != 0)
                    DoubleSpawn();
                if (!switchOver && accuracy != 0)
                    destroyed = true;
            }
            if (Input.GetKeyDown(KeyCode.D) && indexNote == 3 && !switchOver && canDie)
            {
                checkAccuracy();
                if (accuracy != 0)
                    DoubleSpawn();
                if (!switchOver && accuracy != 0)
                    destroyed = true;
            }

            //Move towards location
            if (!switchOver)
                transform.position = Vector3.MoveTowards(transform.position, center.transform.position, speed * Time.deltaTime);
        }
    }

    IEnumerator FadeIn()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        while (sprite.color.a < 1)
        {
            Color temp = sprite.color;
            temp.a = Mathf.Lerp(temp.a, 1, Time.deltaTime / 1.25f);
            sprite.color = temp;
            yield return new WaitForEndOfFrame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Sets up when the note can be dieded
        //if (other.gameObject.name == "hitbox")
        //{
        //    if (other.transform.parent.name == "w_key" && canDie)
        //        trigW = true;
        //    if (other.transform.parent.name == "s_key" && canDie)
        //        trigS = true;
        //    if (other.transform.parent.name == "a_key" && canDie)
        //        trigA = true;
        //    if (other.transform.parent.name == "d_key" && canDie)
        //        trigD = true;
        //}

        //Timing is key
        accuracy++;

        //Otherwise you miss
        if (accuracy == 3)
        {
            scriptNote.miss++;
            scriptNote.multiplier = 0;
            Destroy(gameObject);
            scriptNote.score.subScore(5);
            GameObject ez = Instantiate(accShow, accShow.transform.position, Quaternion.identity);
            ez.GetComponent<SpriteRenderer>().sprite = ez.GetComponent<AccuracyShower>().miss;
        }
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.name == "hitbox")
    //    {
    //        if (other.transform.parent.name == "w_key" && canDie)
    //            trigW = true;
    //        if (other.transform.parent.name == "s_key" && canDie)
    //            trigS = true;
    //        if (other.transform.parent.name == "a_key" && canDie)
    //            trigA = true;
    //        if (other.transform.parent.name == "d_key" && canDie)
    //            trigD = true;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    //Once key leaves, you can no longer press (Probably not needed but added for safety)
    //    trigW = false;
    //    trigS = false;
    //    trigA = false;
    //    trigD = false;
    //}

    //Checks the score and allocates the amount while updating the UI
    void checkAccuracy()
    {
        if (accuracy == 0)
        {
            if (canDie)
                scriptNote.miss++;
            scriptNote.multiplier = 0;
            GameObject ez = Instantiate(accShow, accShow.transform.position, Quaternion.identity);
            ez.GetComponent<SpriteRenderer>().sprite = ez.GetComponent<AccuracyShower>().miss;
            scriptNote.score.subScore(5);
        }

        else if (accuracy == 1)
        {
            scriptNote.multiplier++;
            scriptNote.great++;
            scriptNote.score.addScore(5 * scriptNote.mult);
        }
        else if (accuracy == 2)
        {
            scriptNote.multiplier++;
            scriptNote.perfect++;
            scriptNote.score.addScore(10 * scriptNote.mult);
        }
    }

    void checkAccuracy(GameObject change)
    {
        AccuracyShower script = change.GetComponent<AccuracyShower>();
        if (accuracy == 0 || accuracy == 3)
        {
            script.GetComponent<SpriteRenderer>().sprite = script.miss;
        }
    }

    //When destroyed, removes itself from the front of the list.
    void OnDestroy()
    {
        //if (!destroyed)
            scriptNote.keyNotes[indexNote].Remove(gameObject);
    }

    //Will spawn a double note if it is a double.
    bool DoubleSpawn()
    {
        if (gameObject.name == "w_double(Clone)" || gameObject.name == "s_double(Clone)")
        {
            scriptNote.multiplier++;
            ws = true;
            if (gameObject.name == "w_double(Clone)")
                noteSprite.sprite = doubleSprite[0];
            else
                noteSprite.sprite = doubleSprite[1];

        }
        else if (gameObject.name == "a_double(Clone)" || gameObject.name == "d_double(Clone)")
        {
            scriptNote.multiplier++;
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
        //startSlerp = Time.time;

        return switchOver;
    }

    //Handles the events for a double note.
    void doubleNoteEvent(GameObject keyLoc, KeyCode input)
    {
        //if w or s key
        if (ws)
        {
            if (Mathf.Abs(transform.position.x - keyLoc.transform.position.x) < 0.015f)
            {
                timerDouble -= Time.deltaTime;
                if (timerDouble <= 0)
                {
                    scriptNote.miss++;
                    scriptNote.multiplier = 0;
                    scriptNote.score.subScore(5);
                    scriptNote.dub--;
                    Destroy(gameObject);
                }
            }
            else
                transform.position = Vector3.Slerp(transform.position, keyLoc.transform.position, Time.deltaTime * speed * 3f);
            if (Mathf.Abs(transform.position.x - keyLoc.transform.position.x) <= 0.35f)
            {
                if (Input.GetKeyDown(input))
                {
                    accuracy = 2;
                    checkAccuracy();
                    scriptNote.dub--;
                    Destroy(gameObject);
                }
            }
        }
        else if (ad)
        {
            if (Mathf.Abs(transform.position.y - keyLoc.transform.position.y) < 0.015f)
            {
                timerDouble -= Time.deltaTime;
                if (timerDouble <= 0)
                {
                    scriptNote.miss++;
                    scriptNote.multiplier = 0;
                    scriptNote.score.subScore(5);
                    scriptNote.dub--;
                    Destroy(gameObject);
                }
            }
            else
                transform.position = Vector3.Slerp(transform.position, keyLoc.transform.position, Time.deltaTime * speed * 3f);
            if (Mathf.Abs(transform.position.y - keyLoc.transform.position.y) <= 0.35f)
            {
                if (Input.GetKeyDown(input))
                {
                    accuracy = 2;
                    checkAccuracy();
                    scriptNote.dub--;
                    Destroy(gameObject);
                }
            }
        }
    }
}
