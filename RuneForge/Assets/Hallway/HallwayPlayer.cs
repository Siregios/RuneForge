using UnityEngine;
using System.Collections;

public class HallwayPlayer : MonoBehaviour {

    public GameObject door;
    public float speed = 1f;

	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x - .1f, gameObject.transform.position.y, gameObject.transform.position.z);
            this.transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x + .1f, gameObject.transform.position.y, gameObject.transform.position.z);
            this.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(door != null)
            {
                //gameObject.transform.position = new Vector3(door.GetComponent<Door>().connected.transform.position.x, door.GetComponent<Door>().connected.transform.position.y, door.GetComponent<Door>().connected.transform.position.z);
                this.transform.position = door.GetComponent<Door>().connected.transform.position;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Door"))
            door = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Door"))
            door = null;
    }
}
