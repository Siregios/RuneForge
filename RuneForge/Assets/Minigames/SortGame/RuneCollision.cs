using UnityEngine;
using System.Collections;

public class RuneCollision : MonoBehaviour {

    public GameObject GameManager;
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
            //Reset character
            foreach (Transform child in other.gameObject.transform.parent.GetComponentsInChildren<Transform>())
            {                
                if (child.gameObject.tag == "Character")
                {                   
                    child.transform.position = new Vector3(0, managerScript.charY, 0);
                }
            }
            //Allocate score and subtract current spawn, remove children and destroy rune object
            managerScript.score += 10;
            managerScript.currentSpawn--;            
            other.gameObject.transform.parent.gameObject.SetActive(false);
            Destroy(other.gameObject);
            other.gameObject.transform.parent.DetachChildren();
            managerScript.resetPosition();
        }
    }
}
