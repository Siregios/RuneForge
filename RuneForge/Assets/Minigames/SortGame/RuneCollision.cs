using UnityEngine;
using System.Collections;

public class RuneCollision : MonoBehaviour {

    public GameObject GameManager;
    public GameObject matched;
    public GameObject unmatched;
    SortGameManager managerScript;
    float timeToDel = 0.8f;

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
        managerScript.AudioManager.PlaySound((int)Random.Range(0, 2));
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
                child.GetComponent<SortMove>().timer = child.GetComponent<SortMove>().timerSet;
                child.GetComponent<SortMove>().changeColor = false;
            }
        }
        //After wait, reset the character n such.
        yield return new WaitForSeconds(timeToDel);

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
        managerScript.AudioManager.PlaySound((int)Random.Range(3, 6));
        //Literally same as above code but without adding score and fail animation
        managerScript.score.subScore(5);
        managerScript.resetPosition();
        other.gameObject.SetActive(false);
        GameObject delMatch = (GameObject)Instantiate(unmatched, other.transform.position, Quaternion.identity);
        foreach (Transform child in other.gameObject.transform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Character")
            {
                child.GetComponent<Animator>().SetBool("Fail", true);
                child.GetComponent<SortMove>().timer = child.GetComponent<SortMove>().timerSet;
                child.GetComponent<SortMove>().changeColor = false;
            }
        }
        yield return new WaitForSeconds(timeToDel);
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
