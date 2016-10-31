using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 1f;

    Rigidbody2D rigidBody;

    private Door door;

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
            if (door != null)
            {
                //Bring up UI to select different rooms in the house
                MasterGameManager.instance.sceneManager.LoadScene(door.nextScene);
                //Vector2 nextLocation = door.nextDoor.transform.position;
                //rigidBody.MovePosition(nextLocation);
                //Camera.main.transform.position = new Vector3(door.nextDoor.cameraLocation.x, door.nextDoor.cameraLocation.y, -10);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Door"))
            door = col.GetComponent<Door>();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Door"))
            door = null;
    }
}
