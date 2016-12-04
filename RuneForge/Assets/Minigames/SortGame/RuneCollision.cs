using UnityEngine;
using System.Collections;

public class RuneCollision : MonoBehaviour {

    public GameObject GameManager;
    public GameObject matched;
    SortGameManager managerScript;

	void Start () {
        managerScript = GameManager.GetComponent<SortGameManager>();
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check to see if correct collision with right rune
        if (gameObject.tag == other.gameObject.tag)
        {            
            StartCoroutine(Animation_Success(other));            
        }
        else if (other.gameObject.transform.parent.name != "ItemSet")
        {
            StartCoroutine(Animation_Failure(other));
        }
    }

    IEnumerator Animation_Success(Collider2D other)
    {
        //This code will allocate score first and reset cursor and object position.
        managerScript.score.addScore(25);
        managerScript.resetPosition();
        other.gameObject.SetActive(false);
        GameObject delMatch = (GameObject)Instantiate(matched, other.transform.position, Quaternion.identity);

        //Play animation
        foreach (Transform child in other.gameObject.transform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Character")
            {
                child.GetComponent<Animator>().SetBool("Success", true);
            }
        }
        //After wait, reset the character n such.
        yield return new WaitForSeconds(.8f);

        //Reset character
        foreach (Transform child in other.gameObject.transform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Character")
            {
                child.GetComponent<SortMove>().moveDown = true;
            }
        }
        //Allocate score and subtract current spawn, remove children and destroy rune object            
        managerScript.currentSpawn--;
        other.gameObject.transform.parent.gameObject.SetActive(false);
        Destroy(other.gameObject);
        Destroy(delMatch);
        other.gameObject.transform.parent.DetachChildren();
    }

    IEnumerator Animation_Failure(Collider2D other)
    {
        //Literally same as above code but without adding score and fail animation
        managerScript.score.subScore(5);
        managerScript.resetPosition();
        other.gameObject.SetActive(false);
        GameObject delMatch = (GameObject)Instantiate(matched, other.transform.position, Quaternion.identity);
        foreach (Transform child in other.gameObject.transform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Character")
            {
                child.GetComponent<Animator>().SetBool("Fail", true);
            }
        }
        yield return new WaitForSeconds(.8f);
        foreach (Transform child in other.gameObject.transform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Character")
            {
                child.GetComponent<SortMove>().moveDown = true;
            }
        }
        managerScript.currentSpawn--;
        other.gameObject.transform.parent.gameObject.SetActive(false);
        Destroy(other.gameObject);
        Destroy(delMatch);
        other.gameObject.transform.parent.DetachChildren();
    }
}
