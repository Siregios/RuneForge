using UnityEngine;
using System.Collections;

public class StackingPlayerMovement : MonoBehaviour {

    public float playerSpeed = 10.0f;
    public float leftBound = -8f;
    public float rightBound = 8f;
    private bool moveRight, moveLeft;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x > leftBound && Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * playerSpeed * Time.deltaTime;
            moveRight = true;
        }
        else
            moveRight = false;

        if (transform.position.x < rightBound && Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * playerSpeed * Time.deltaTime;
            moveLeft = true;
        }
        else
            moveLeft = false;

    }

    void FixedUpdate()
    {
        if (moveLeft || moveRight)
        {
            foreach (Transform child in transform)
            {
                int direction = moveLeft ? 1 : -1;
                child.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((direction * (playerSpeed/2)), 0.0f));
            }
        }
    }
}
