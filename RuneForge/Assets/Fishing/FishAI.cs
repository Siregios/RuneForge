using UnityEngine;
using System.Collections;

public class FishAI : MonoBehaviour {

    //Controls speed of fish and the amount of time the fish pauses.
    public float speed = 5f;
    private float pause = 1.5f;

    //Records the last pause fish had and variable for periods between pauses.
    private float lastPause;
    public float pauseBetween = 2;

    //Makes fish move for x amount of frames.
    private int frameDuration = 0;
    private int amountOfFrames = 60;
    private bool direction = true;
    Vector3 pos;


    // Use this for initialization
    void Start() {
        lastPause = Time.time;
    }

    // Update is called once per frame
    void Update() {
        MovementAI();
    }


    //Moves the fish
    private void MovementAI()
    {        
        Debug.Log(frameDuration);
        //checks for last pause and if fish is still in movement.
        if (lastPause < Time.time && frameDuration == 0)
        {
            //95% chance movement or 5% chance pause.
            if (Random.Range(1, 101) <= 95)
            {
                //Moves for "amountOfFrames"
                frameDuration = amountOfFrames;
                //50% chance up or down
                if (Random.Range(1, 101) <= 51)
                {
                    transform.position += transform.up * speed * Time.deltaTime;
                    direction = true;
                }
                else
                {
                    transform.position -= transform.up * speed * Time.deltaTime;
                    direction = false;
                }
            }

            //Pause fish for "pause" seconds
            else if (Time.time - lastPause > pauseBetween)
            {
                lastPause = Time.time + pause;
            }
        }
        //If frames not complete, fish will continue to move that direction
        else if (frameDuration != 0)
        {
            frameDuration--;
            if (direction)
            {
                transform.position += transform.up * speed * Time.deltaTime;
            }
            else
            {
                transform.position -= transform.up * speed * Time.deltaTime;
            }
        }
        
    }
}

