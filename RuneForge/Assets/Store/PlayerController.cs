using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Direction.DIRECTION movingDirection;

    public float speed = 1f;

    Rigidbody2D rigidBody;
    public bool moveByMouse = false;
    Vector3 targetClick;
    public bool tutorial = false;

    PointerEventData pointer = new PointerEventData(EventSystem.current);
    System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();

    void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// TODO : Find a way to only open interactable when player reaches that destination
    /// Also deal with never being able to reach that interactable if something is in the way (e.g. Quest Board)
    /// </summary>
    //RaycastHit2D hit;
    //Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //hit = Physics2D.Raycast(clickPos, Vector2.zero);
    //if (hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactable"))
    //    Debug.Log(hit.collider.gameObject.layer);

    void Update()
    {
        if (!MasterGameManager.instance.uiManager.uiOpen && MasterGameManager.instance.inputActive)
        {
            CheckForMove();
        }
    }

    void FixedUpdate()
    {
        if (!MasterGameManager.instance.uiManager.uiOpen)
        {
            Move();
        }
        else if (tutorial)
        {
            Move();
        }
        else
        {
            movingDirection = Direction.DIRECTION.NONE;
            moveByMouse = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
            MasterGameManager.instance.sceneManager.LoadScene(other.GetComponent<Door>().nextScene);
    }

    void CheckForMove()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            movingDirection = Direction.DIRECTION.LEFT;
            moveByMouse = false;
        }
        else if (Input.GetKeyUp(KeyCode.A) && movingDirection == Direction.DIRECTION.LEFT)
            movingDirection = Direction.DIRECTION.NONE;

        if (Input.GetKeyDown(KeyCode.D))
        {
            movingDirection = Direction.DIRECTION.RIGHT;
            moveByMouse = false;
        }
        else if (Input.GetKeyUp(KeyCode.D) && movingDirection == Direction.DIRECTION.RIGHT)
            movingDirection = Direction.DIRECTION.NONE;

        if (Input.GetKeyDown(KeyCode.Mouse0) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            pointer.position = Input.mousePosition;
            results.Clear();
            EventSystem.current.RaycastAll(pointer, results);
            if (results.Count == 0 && !MasterGameManager.instance.interactionManager.isHovering)
            {
                moveByMouse = true;
                Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetClick = new Vector2(clickPos.x, this.transform.position.y);
                movingDirection = (clickPos.x < this.transform.position.x) ? Direction.DIRECTION.LEFT : Direction.DIRECTION.RIGHT;
            }
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "VerticalBounds")
        {
            moveByMouse = false;
            movingDirection = Direction.DIRECTION.NONE;
        }
    }

    void Move()
    {
        Vector3 playerPosition = transform.position;
        if (moveByMouse)
        {
            if (Mathf.Abs(playerPosition.x - targetClick.x) > 0.1f)
                this.transform.position = Vector2.MoveTowards(playerPosition, targetClick, speed * Time.deltaTime);
            else
            {
                movingDirection = Direction.DIRECTION.NONE;
                moveByMouse = false;
            }
        }
        else
        {
            switch (movingDirection)
            {
                case Direction.DIRECTION.LEFT:
                    rigidBody.MovePosition(playerPosition + new Vector3(-speed * Time.deltaTime, 0, 0));
                    break;
                case Direction.DIRECTION.RIGHT:
                    rigidBody.MovePosition(playerPosition + new Vector3(speed * Time.deltaTime, 0, 0));
                    break;
            }
        }
    }
}

///Deprecated
//void Update()
//{
//    //Update the player position erryday
//    //if (door != null)
//    //{
//    //    MasterGameManager.instance.sceneManager.LoadScene(door.nextScene);
//    //}

//    //If key is A or D, move player that direction
//    //mouseclick false and velocity zero in case player tries to click and use keys at the same time, keys take priority.
//    if (Input.GetKeyDown(KeyCode.A))
//    {
//        movingDirection = Direction.DIRECTION.LEFT;
//        //this.transform.Translate(-speed * Time.deltaTime, 0, 0);
//        //rigidBody.position = (playerPosition + new Vector3(-speed * Time.deltaTime, 0, 0));
//        //rigidBody.velocity = Vector3.zero;
//        //mouseClick = false;
//    }
//    else if (Input.GetKeyUp(KeyCode.A) && movingDirection == Direction.DIRECTION.LEFT)
//        movingDirection = Direction.DIRECTION.NONE;
//    if (Input.GetKeyDown(KeyCode.D))
//    {
//        movingDirection = Direction.DIRECTION.RIGHT;
//        //this.transform.Translate(speed * Time.deltaTime, 0, 0);
//        //rigidBody.MovePosition(playerPosition + new Vector3(speed * Time.deltaTime, 0, 0));
//        //rigidBody.velocity = Vector3.zero;
//        //mouseClick = false;
//    }
//    else if (Input.GetKeyUp(KeyCode.D) && movingDirection == Direction.DIRECTION.RIGHT)
//        movingDirection = Direction.DIRECTION.NONE;
//    //if (!MasterGameManager.instance.uiManager.uiOpen &&
//    //    Input.GetKeyDown(KeyCode.Mouse0) && (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
//    //{
//    //    targetClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//    //    targetClick = new Vector3(targetClick.x, playerPosition.y, playerPosition.z);
//    //    mouseClick = true;
//    //}
//}