using UnityEngine;
using System.Collections;

public class movementAI : MonoBehaviour
{

    //Controls speed of fish and the amount of time the fish pauses.
    public float speed;
    public float pause = 1.5f;
    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    //Records the last pause fish had and variable for periods between pauses.
    private float lastPause;
    public float pauseBetween = 2;

    //Controls whether the fish moves up or down for a certain random amount of time.
    public bool vertical = false;
    public bool horizontal = false;
    private bool up = false;
    private bool left = false;
    private bool right = false;
    private bool down = false;
    private int options = 0;

    //Amount of movement in random direction
    private float randTime = 0;
    public float minTime = 1;
    public float maxTime = 5f;

    void Start()
    {
        lastPause = Time.time;

    }

    void Update()
    {
        //subtracts the time while fish is in motion until it reaches 0.
        randTime -= Time.deltaTime;

        //When fish is done moving, check what it will do next.
        if (randTime <= 0)
            MovementAI();
        else
        {
            if (vertical)
            {
                if (up)
                    MoveUp();
                else if (down)
                    MoveDown();
            }
            if (horizontal)
            {
                if (right)
                    MoveRight();
                else if (left)
                    MoveLeft();
            }
        }
    }

    //Movement handler
    private void MovementAI()
    {
        //checks for last pause.
        if (lastPause < Time.time)
        {
            //chance of movement and pausing.
            if (Random.Range(1, 101) <= 70)
            {
                if (vertical && horizontal)
                {
                    randTime = Random.Range(minTime, maxTime);
                    speed = Random.Range(minSpeed, maxSpeed);
                    options = Random.Range(1, 4);
                    if (options == 1)
                        up = true;
                    else if (options == 2)
                        down = true;

                    options = Random.Range(1, 4);
                    if (options == 1)
                        right = true;
                    else if (options == 2)
                        left = true;   
                }
                //50% chance up or down
                else if (vertical)
                {
                    randTime = Random.Range(minTime, maxTime);
                    speed = Random.Range(minSpeed, maxSpeed);
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
                
                //50% chance left or right
                else if (horizontal)
                {
                    randTime = Random.Range(minTime, maxTime);
                    speed = Random.Range(minSpeed, maxSpeed);
                    if (Random.Range(1, 101) <= 51)
                    {
                        right = true;
                        left = false;
                    }
                    else
                    {
                        right = true;
                        left = false;
                    }
                }
            }

            //Pause for "pause" seconds
            else if (Time.time - lastPause > pauseBetween)
            {
                lastPause = Time.time + pause;
                up = down = false;
            }
        }
    }

    private void MoveUp()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void MoveDown()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
    }

    private void MoveLeft()
    {
        transform.position -= transform.right * speed * Time.deltaTime;
    }

    private void MoveRight()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    //Makes the object go opposite direction for remainder of travel time.
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "AI")
        {
            Physics2D.IgnoreCollision(other.collider, this.GetComponent<Collider2D>());
        }
        else if (other.gameObject.tag == "HorizontalBounds")
        {
            if (right)
            {
                right = false;
                left = true;
            }
            else if (left)
            {
                right = true;
                left = false;
            }
        }
        else if (other.gameObject.tag == "VerticalBounds")
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
}

