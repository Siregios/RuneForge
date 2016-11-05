using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 1f;

    Rigidbody2D rigidBody;
    private Door door;
    public Vector3 playerPosition;
    //For left click movement    
    public bool mouseClick = false;
    public Vector3 targetClick;

    void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

	void Update () {
        //Update the player position erryday
        playerPosition = transform.position;
        if (door != null)
        {
            MasterGameManager.instance.sceneManager.LoadScene(door.nextScene);
        }

        //If key is A or D, move player that direction
        //mouseclick false and velocity zero in case player tries to click and use keys at the same time, keys take priority.
        if (Input.GetKey(KeyCode.A))
        {            
            rigidBody.MovePosition(playerPosition + new Vector3(-speed * Time.deltaTime, 0, 0));
            rigidBody.velocity = Vector3.zero;
            mouseClick = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.MovePosition(playerPosition + new Vector3(speed * Time.deltaTime, 0, 0));
            rigidBody.velocity = Vector3.zero;
            mouseClick = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetClick = new Vector3(targetClick.x, playerPosition.y, playerPosition.z);
            mouseClick = true;
        }
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    if (door != null)
        //    {
        //        //Bring up UI to select different rooms in the house
        //        MasterGameManager.instance.sceneManager.LoadScene(door.nextScene);
        //        //Vector2 nextLocation = door.nextDoor.transform.position;
        //        //rigidBody.MovePosition(nextLocation);
        //        //Camera.main.transform.position = new Vector3(door.nextDoor.cameraLocation.x, door.nextDoor.cameraLocation.y, -10);
        //    }
        //}
    }

    void FixedUpdate()
    {
        //If mouse click, move rigidbody 
        if (mouseClick)
        {
            Vector3 direction = (targetClick - playerPosition).normalized * speed;
            rigidBody.velocity = direction;
            if (Mathf.Abs(playerPosition.x - targetClick.x) < 2f)
            {
                rigidBody.velocity = Vector3.zero;
                playerPosition = targetClick;
                mouseClick = false;
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "VerticalBounds")
        {            
            mouseClick = false;
            rigidBody.velocity = Vector3.zero;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        OnCollisionEnter2D(other);
    }    
    
}
