using UnityEngine;
using System.Collections;

public class RuneCollision : MonoBehaviour {

    public GameObject GameManager;
    public GameObject matched;
    SortGameManager managerScript;
	// Use this for initialization
	void Start () {
        managerScript = GameManager.GetComponent<SortGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check to see if correct collision with right rune
        if (gameObject.tag == other.gameObject.tag)
        {            
            StartCoroutine(Animation_Success(other));            
        }
    }

    IEnumerator Animation_Success(Collider2D other)
    {
        //This code will allocate score first and reset cursor and object position.
        managerScript.score.addScore(25);
        managerScript.resetPosition();
        other.gameObject.SetActive(false);
        GameObject delMatch = (GameObject)Instantiate(matched, other.transform.position, Quaternion.identity);
        foreach (Transform child in other.gameObject.transform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Character")
            {
                child.GetComponent<Animator>().SetBool("Success", true);
                //child.transform.Translate(Vector3.down * Time.deltaTime * 5f);
            }
        }
        //After wait, reset the character n such.
        yield return new WaitForSeconds(1);

        //Reset character
        foreach (Transform child in other.gameObject.transform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Character")
            {
                child.transform.position = new Vector3(0, managerScript.charY, 0);
            }
        }
        //Allocate score and subtract current spawn, remove children and destroy rune object            
        managerScript.currentSpawn--;
        other.gameObject.transform.parent.gameObject.SetActive(false);
        Destroy(other.gameObject);
        Destroy(delMatch);
        other.gameObject.transform.parent.DetachChildren();
    }

    IEnumerator Animation_Failure()
    {
        yield return new WaitForSeconds(1f);
    }
}
