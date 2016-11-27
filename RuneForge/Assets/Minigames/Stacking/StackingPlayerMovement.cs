using UnityEngine;
using System.Collections;

public class StackingPlayerMovement : MonoBehaviour {

    public float playerSpeed = 10.0f;
    public float leftBound = -8f;
    public float rightBound = 8f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(transform.position.x > leftBound && Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * playerSpeed * Time.deltaTime;
        }

        if (transform.position.x < rightBound && Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * playerSpeed * Time.deltaTime;
        }

    }
}
