using UnityEngine;
using System.Collections;

public class HallwayPlayer : MonoBehaviour {

    public GameObject door;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - .1f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + .1f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(door != null)
            {
                gameObject.transform.position = new Vector3(door.GetComponent<Door>().connected.transform.position.x, door.GetComponent<Door>().connected.transform.position.y, door.GetComponent<Door>().connected.transform.position.z);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        door = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        door = null;
    }
}
