using UnityEngine;
using System.Collections;

public class ChargeSelector : MonoBehaviour {

    private bool inSelection = false;   //Is the marker in the green?
    private bool buttonMash = false;    //True if game is in button mash mode
    //may replace later
    private bool isPlaying = true;      //False during animations and wait times

       
    private int mashCount = 0;
    private float lastSpeed;
    private float timeRemaining;
    private float targetCenter;
    public Score score;

    public float startXPos = -6.5f;
    public float startYPos = -2.0f;
    public int maxSpeed = 30;
    public float speedIncrement = 2.0f;
    public float chargeTime = 5.0f;
    public float randTime = 5.0f;
    public float greenWidth = .3f;
    public float yellowWidth = .45f;


    private TargetMovment target;
    private movementAI movement;
    private SmithingBlacksmithAnimations blacksmith;


	// Use this for initialization
	void Start () {
        timeRemaining = chargeTime;

        target = GameObject.Find("Target").GetComponent<TargetMovment>();
        targetCenter = target.getCenterX();
        
        movement = this.GetComponent<movementAI>();
        lastSpeed = movement.minSpeed;

        blacksmith = GameObject.Find("Blacksmith").GetComponent<SmithingBlacksmithAnimations>();


    }
	
	// Update is called once per frame
	void Update () {
        if (timeRemaining <= 0)
            timeRemaining = 0;
        else
            timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            if (buttonMash)
            {
                if (isPlaying)
                {
                    StartCoroutine(waitForAnimation());
                    resetMarker();
                    blacksmith.stopHammering();
                    blacksmith.resetSpeed();
                }

            }
            //else
                //resetMarker();
        }

        if (selectButtonDown() && isPlaying)
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

    //Reset position of marker. If scored, add to score
    void checkForScore()
    {
        stopMarker();
        targetCenter = target.getCenterX();
        //lightning[0].enabled = true;
        blacksmith.startHammering();
        if (inSelection)
        {
            blacksmith.hit = true;
            //is the target in the green?
            //Debug.Log(transform.position.x);
            //Debug.Log(targetCenter);
            if (this.transform.position.x > (targetCenter - greenWidth) && this.transform.position.x < (targetCenter + greenWidth))
            {
                score.addScore(50);
                //Debug.Log("Perfect!");
            }
            score.addScore(50);
            //Debug.Log("RIDE THE LIGHTNING!");
            buttonMash = true;
            timeRemaining = chargeTime;
        }
        else
        {
            //Debug.Log("MISS!");
            blacksmith.hit = false;
            StartCoroutine(waitForAnimation());
        }


    }

    void mashUp()
    {
        //Debug.Log(mashCount);
        mashCount++;
        blacksmith.increaseSpeed();
        score.addScore(2);
        //to be replaced
        if (((float)mashCount / 25) * 100 > 50)
        {
            score.addScore(3);
        }
        if (((float)mashCount / 25) * 100 > 75)
        {
            score.addScore(5);
        }
    }

    //stop the marker's movement and save its speed
    void stopMarker()
    {
        lastSpeed = movement.speed;
        movement.maxSpeed = 0;
        movement.minSpeed = 0;
        movement.speed = 0;
    }

    //resets the marker and sets a new positon for the target
    void resetMarker()
    {
        transform.position = new Vector2(startXPos, startYPos);
        if(lastSpeed + speedIncrement <= maxSpeed)
        {
            movement.maxSpeed = lastSpeed + speedIncrement;
            movement.minSpeed = lastSpeed + speedIncrement;
            movement.speed = lastSpeed + speedIncrement;
        }
        else
        {
            movement.maxSpeed = lastSpeed;
            movement.minSpeed = lastSpeed;
            movement.speed = lastSpeed;
        }
        target.changePosition();
        timeRemaining = 0;
    }

    //may be replaced
    IEnumerator waitForAnimation()
    {
        isPlaying = false;
        yield return new WaitForSeconds(0.5f);
        //to be replaced
        isPlaying = true;
        if (!buttonMash)
        {
            resetMarker();
            blacksmith.stopHammering();
        }
        else
        {
            buttonMash = false;
            mashCount = 0;
        }
    }

    public float getTimeRemaining()
    {
        return timeRemaining;
    }

    public int getScore()
    {
        return score.score;
    }

    public string getMode()
    {
        return buttonMash ? "Fire" : "Aim";
    }
}
