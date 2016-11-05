using UnityEngine;
using System.Collections;

public class ChargeSelector : MonoBehaviour {

    private bool inSelection = false;   //Is the pointer in the green?
    private bool buttonMash = false;    //True if game is in button mash mode

    private int mashCount = 0;
    private float lastSpeed;
    public float startXPos = -6.5f;
    public float startYPos = -2.0f;
    public int score = 0;
    public int maxSpeed = 30;
    public float speedIncrement = 2.0f;

    private TargetMovment target;
    private movementAI movement;

	// Use this for initialization
	void Start () {

        target = GameObject.Find("Target").GetComponent<TargetMovment>();
        movement = this.GetComponent<movementAI>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (selectButtonDown())
        {
            if (buttonMash)
                mashUp();
            else
                checkForScore();
        }
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        inSelection = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        inSelection = false;
    }

    bool selectButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
            return true;
        if (Input.GetKeyDown(KeyCode.Space))
            return true;
        return false;
    }

    //Reset position of pointer. If scored, add to score
    void checkForScore()
    {
        if (inSelection)
        {
            score++;
            if (movement.speed < maxSpeed)
            {
                lastSpeed = movement.speed;
                movement.maxSpeed = 0;
                movement.minSpeed = 0;
                movement.speed = 0;
            }
            buttonMash = true;
        }
        //reset pointer on button push

    }

    void mashUp()
    {
        Debug.Log(mashCount);
        mashCount++;
        if (mashCount == 25)    //After some number/time of button mashing, reset and increase speed
        {
            buttonMash = false;
            movement.maxSpeed = lastSpeed + speedIncrement;
            movement.minSpeed = lastSpeed + speedIncrement;
            movement.speed = lastSpeed + speedIncrement;
            target.changePosition();
            mashCount = 0;
            transform.position = new Vector2(startXPos, startYPos);
        }
    }
}
