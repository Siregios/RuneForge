using UnityEngine;
using System.Collections;

public class FishAI : MonoBehaviour {

    //Controls speed of fish and the amount of time the fish pauses.
    public float speed = 2f;
    private float pause = 1.5f;

    //Records the last pause fish had and variable for periods between pauses.
    private float lastPause;
    public float pauseBetween = 2;
    
    //Controls whether the fish moves up or down for a certain random amount of time.
    private bool up = false;
    private bool down = false;
    private float randTime = 0;
    private float minTime = 1;
    private float maxTime = 5f;

    void Start() {
        lastPause = Time.time;

    }

    void Update() {
        //subtracts the time while fish is in motion until it reaches 0.
        randTime -= Time.deltaTime;

        //When fish is done moving, check what it will do next.
        if (randTime <= 0)
        {
            MovementAI();
        }
        
        else if (up)
        {
            MoveUp();
        }
        else if (down)
        {
            MoveDown();
        }        
    }

    //Moves the fish
    private void MovementAI()
    {                
        //checks for last pause.
        if (lastPause < Time.time)
        {            
            //chance of movement and pausing.
            if (Random.Range(1, 101) <= 70)
            {
                //50% chance up or down
                randTime = Random.Range(minTime, maxTime);
                speed = Random.Range(1, 3);
                if (Random.Range(1, 101) <= 51)
                {
                    up = true;
                    down = false;                    
                }
                else
                {
                    down = true;
                    up = false;
                }
            }

            //Pause fish for "pause" seconds
            else if (Time.time - lastPause > pauseBetween)
            {
                lastPause = Time.time + pause;
                up = down = false;
            }
        }                        
    }

    //Moves fish up
    private void MoveUp()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    //Moves fish down
    private void MoveDown()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (up)
        {
            up = false;
            down = true;
        }
        else if (down)
        {
            up = true;
            down = false;
        }
    }
}

