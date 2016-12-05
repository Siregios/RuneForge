using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortMove : MonoBehaviour {

    public bool moveUp;
    public bool moveDown;    
    public float timerSet = 3f;
    public float timer;    
    public GameObject GameManager;
    public GameObject matched;
    SortGameManager managerScript;

    void Start()
    {
        timer = timerSet;
        managerScript = GameManager.GetComponent<SortGameManager>();
    }

    void Update () {
        if (Mathf.Abs(transform.position.y + 3.5f) < 0.2f)
            timer -= Time.deltaTime;

        if (timer < 0)
        {            
            GetComponent<Animator>().SetBool("Fail", true);
            foreach (Transform child in transform.parent.GetComponentsInChildren<Transform>())
            {
                if (child.gameObject.tag == "Red" || child.gameObject.tag == "Blue" || child.gameObject.tag == "Green" || child.gameObject.tag == "Yellow")
                {
                    Destroy(child.gameObject);                    
                }
            }
            if (timer <= -0.8f)
            {                
                managerScript.score.subScore(5);
                moveDown = true;
                managerScript.currentSpawn--;
                transform.parent.gameObject.SetActive(false);
                transform.parent.DetachChildren();
                timer = timerSet;
            }
        }
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

        if (moveDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -7f, 0), Time.deltaTime * 12f);
            if (Mathf.Abs(transform.position.y + 7) < 0.2f)
            {
                moveDown = false;
                transform.position = new Vector3(transform.position.x, -7f, 0);
            }
        }

    }
}
