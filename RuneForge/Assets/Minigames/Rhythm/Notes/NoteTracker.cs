using UnityEngine;
using System.Collections;

public class NoteTracker : MonoBehaviour {
    public int accuracy = 0;
    public float speed = 3.5f;
    public RandomSpawnNote scriptNote;
    public GameObject center;
    public bool canDie = false;
    bool trigW, trigS, trigA, trigD = false;

	void Start () {
        scriptNote = GameObject.Find("RandomSpawn").GetComponent<RandomSpawnNote>();
        center = GameObject.Find("center");
	}
	
	
	void Update () {
        if (gameObject == scriptNote.keyNotes[0])
        {
            canDie = true;
        }
        if (Input.GetKeyDown(KeyCode.W) && trigW)
        {
            checkAccuracy();
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.S) && trigS)
        {
            checkAccuracy();
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.A) && trigA)
        {
            checkAccuracy();
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.D) && trigD)
        {
            checkAccuracy();
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, center.transform.position, speed * Time.deltaTime);
        
    }            

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "hitbox")
            if (other.transform.parent.name == "w_key" && canDie)
                trigW = true;
            if (other.transform.parent.name == "s_key" && canDie)
                trigS = true;
            if (other.transform.parent.name == "a_key" && canDie)
                trigA = true;
            if (other.transform.parent.name == "d_key" && canDie)
                trigD = true;

        if (other.gameObject.name != "hitbox")
            accuracy++;

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
        trigW = false;
        trigS = false;
        trigA = false;
        trigD = false;
    }

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

    void OnDestroy()
    {
        scriptNote.keyNotes.Remove(gameObject);
    }
}
