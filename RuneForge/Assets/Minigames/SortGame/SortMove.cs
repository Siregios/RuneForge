using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortMove : MonoBehaviour {

    public bool moveUp = false;
    public bool moveDown = false;
    public bool changeColor = true;
    public float timerSet = 3f;
    public float timer;
    private bool soundPlayed;
    public GameObject GameManager;
    public GameObject unmatched;
    bool matchedOn = true;
    SortGameManager managerScript;
    float timeToDel = 0.8f;
    Color spriteColor;

    void Start()
    {
        //set variables
        timer = timerSet;
        managerScript = GameManager.GetComponent<SortGameManager>();
        spriteColor = GetComponent<SpriteRenderer>().color;
        soundPlayed = false;
    }

    void Update () {
        //Subtracts timer and change color over time
        if (Mathf.Abs(transform.position.y + 3.5f) < 0.2f)
        {
            timer -= Time.deltaTime;
            if (changeColor)
                GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.red, Time.deltaTime / (timerSet - 1));
            else
                GetComponent<SpriteRenderer>().color = spriteColor;
        }

        //When timer is below 0 start the animation and all
        if (timer < 0)
        {
            if (!soundPlayed)
            {
                managerScript.AudioManager.PlaySound((int)Random.Range(3, 6));
                soundPlayed = true;
            }
            GetComponent<SpriteRenderer>().color = spriteColor;
            GetComponent<Animator>().SetBool("Fail", true);
            foreach (Transform child in transform.parent.GetComponentsInChildren<Transform>())
            {
                if (child.gameObject.tag == "Red" || child.gameObject.tag == "Blue" || child.gameObject.tag == "Green" || child.gameObject.tag == "Yellow")
                {
                    Destroy(child.gameObject);
                    if (matchedOn)
                        Destroy((GameObject)Instantiate(unmatched, child.transform.position, Quaternion.identity), timeToDel);
                }
            }
            //This is for instantiate
            matchedOn = false;
            //This is to reset
            if (timer <= -timeToDel)
            {                
                managerScript.score.subScore(5);
                moveDown = true;
                transform.parent.gameObject.SetActive(false);
                transform.parent.DetachChildren();
                matchedOn = true;
                timer = timerSet;
                soundPlayed = false;
            }
        }

        //Moves golem up
        if (moveUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -3.5f, 0), Time.deltaTime * 12f);
            if (Mathf.Abs(transform.position.y + 3.5f) < 0.2f)
            {
                moveUp = false;
                transform.position = new Vector3(transform.position.x, -3.5f, 0);
                Color tmp = transform.parent.gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a = 1f;
                transform.parent.gameObject.GetComponent<SpriteRenderer>().color = tmp;
                foreach (Transform child in transform.parent)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        //Moves golem down
        if (moveDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -7f, 0), Time.deltaTime * 12f);
            if (Mathf.Abs(transform.position.y + 7) < 0.2f)
            {
                changeColor = true;
                moveDown = false;
                transform.position = new Vector3(transform.position.x, -7f, 0);
                managerScript.currentSpawn--;
                managerScript.characterSpawn.Add(managerScript.WAKEMEUPINSIDE[1]);
                managerScript.WAKEMEUPINSIDE.RemoveAt(1);
                managerScript.bubbleSpawn.Add(managerScript.WAKEMEUPINSIDE[0]);
                managerScript.WAKEMEUPINSIDE.RemoveAt(0);
            }
        }

    }
}
