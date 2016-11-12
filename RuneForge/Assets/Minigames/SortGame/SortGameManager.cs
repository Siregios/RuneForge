using UnityEngine;
using System.Collections;

public class SortGameManager : MonoBehaviour {

    public GameObject[] runes;
    public GameObject[] bubbles;
    bool grabbed = false;
    GameObject drag;
    public int currentSpawn = 0;
    Vector3 oldPos;
    bool notDone = true;
    float timeToSpawn = 3f;
    float time;

	void Start () {
        time = timeToSpawn;
	}
		
	void Update () {
        time -= Time.deltaTime;

        //Check if any characters are requesting an item.
        if (currentSpawn < 3 && time <= 0)
        {           
            int randomInt = Random.Range(0, 3);
            if (bubbles[randomInt].activeSelf == false)
            {
                bubbles[randomInt].SetActive(true);
                time = timeToSpawn;
                int randomRune = Random.Range(0, 4);
                GameObject spawnedRune = (GameObject) Instantiate(runes[randomRune], bubbles[randomInt].transform.position, Quaternion.identity);
                spawnedRune.transform.parent = bubbles[randomInt].transform;
                
            }                            
        }

        //On click, check if you clicked on rune by tag and if so then grab it.
	    if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit != null && hit.collider != null)
            {
                if (hit.collider.tag == "Red" || hit.collider.tag == "Yellow" || hit.collider.tag == "Blue" || hit.collider.tag == "Green")
                {
                    grabbed = true;
                    drag = hit.collider.gameObject;
                    oldPos = hit.collider.gameObject.transform.position;
                }

            }
            //Makes cursor invisible
            if (grabbed)
                Cursor.visible = false;
        }

        //While left click down, drag object around.
        if (Input.GetMouseButton(0))
        {
            if (grabbed)
            {
                Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                drag.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0f);
            }
        }

        //On left click let go, object is donezoes.
        if (Input.GetMouseButtonUp(0))
        {
            if (grabbed)
            {
                drag.transform.position = oldPos;
                drag = null;
                Cursor.visible = true;
                grabbed = false;
            }
        }
	}
}
