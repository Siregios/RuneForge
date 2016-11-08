using UnityEngine;
using System.Collections;

public class ChargeSelector : MonoBehaviour {

    private bool inSelection = false;   //Is the marker in the green?
    private bool buttonMash = false;    //True if game is in button mash mode
    //may replace later
    private bool isPlaying = true;      //False during animations and wait times

       
    private int mashCount = 0;
    private float lastSpeed;
    public float startXPos = -6.5f;
    public float startYPos = -2.0f;
    public int score = 0;
    public int maxSpeed = 30;
    public float speedIncrement = 2.0f;

    private TargetMovment target;
    private movementAI movement;

    //These will all be replaced with an animation or particle affect at some point
    private SpriteRenderer[] lightning;

	// Use this for initialization
	void Start () {

        target = GameObject.Find("Target").GetComponent<TargetMovment>();
        movement = this.GetComponent<movementAI>();

        //to be replaced
        lightning = new SpriteRenderer[3];
        lightning[0] = GameObject.Find("LIGHTNING").GetComponent<SpriteRenderer>();
        lightning[1] = GameObject.Find("MORE LIGHTNING").GetComponent<SpriteRenderer>();
        lightning[2] = GameObject.Find("EVEN MORE LIGHTNING").GetComponent<SpriteRenderer>();


    }
	
	// Update is called once per frame
	void Update () {

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
        lightning[0].enabled = true;
        if (inSelection)
        {
            score++;
            Debug.Log("RIDE THE LIGHTNING!");
            buttonMash = true;
        }
        else
        {
            Debug.Log("MISS!");
            //to be replaced
            StartCoroutine(waitForLightning());
        }


    }

    void mashUp()
    {
        //Debug.Log(mashCount);
        mashCount++;

        //to be replaced
        if (((float)mashCount / 25) * 100 > 50)
            lightning[1].enabled = true;
        if (((float)mashCount / 25) * 100 > 75)
            lightning[2].enabled = true;

        if (mashCount == 25)    //After some number/time of button mashing, reset and increase speed
        {
            buttonMash = false;
            mashCount = 0;
            StartCoroutine(waitForLightning());
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
    }

    //may be replaced
    IEnumerator waitForLightning()
    {
        isPlaying = false;
        yield return new WaitForSeconds(1f);
        isPlaying = true;
        //to be replaced
        for (int i = 0; i < 3; i++)
            lightning[i].enabled = false;
        resetMarker();
    }
}
