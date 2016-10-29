using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 1f;

    Rigidbody2D rigidBody;

    private GameObject door;

    void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

	void Update () {

        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.MovePosition(this.transform.position + new Vector3(-speed * Time.deltaTime, rigidBody.velocity.y * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.MovePosition(this.transform.position + new Vector3(speed * Time.deltaTime, rigidBody.velocity.y * Time.deltaTime, 0));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(door != null)
            {
                //This is messing up at the moment because the destination door is scaled.
                Vector2 nextLocation = door.GetComponent<Door>().connected.transform.position;
                rigidBody.MovePosition(nextLocation);
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
