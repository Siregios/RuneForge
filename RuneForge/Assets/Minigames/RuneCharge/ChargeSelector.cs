using UnityEngine;
using System.Collections;

public class ChargeSelector : MonoBehaviour {

    private bool inSelection = false;   //Is the pointer in the green?
    public float startXPos = -6.5f;
    public float startYPos = -2.0f;
    public int score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (selectButtonDown())
            checkForScore();
	
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
            //reset pos of target region
        }
        transform.position = new Vector2(startXPos, startYPos);

    }
}
