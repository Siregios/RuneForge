using UnityEngine;
using System.Collections;

public class StackableBehavior : MonoBehaviour
{
    public float fallSpeed = 10f;

    //this is set by the manager to be true
    private bool isFalling;
    private bool collisionRegistered;
    public bool isTop;

    //Set true when it becomes a part of the stack
    public bool stacked;

    public GameObject player;
    public StackingGameManager stack;

	// Use this for initialization
	void Start ()
    {
        isTop = true;
        stacked = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isFalling)
            transform.position -= transform.up * fallSpeed * Time.deltaTime;
        Debug.Log(this.GetComponent<Rigidbody2D>().velocity.magnitude);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((!isTop && (other.gameObject.GetComponent<StackableBehavior>() != null && !other.gameObject.GetComponent<StackableBehavior>().isTop)) || collisionRegistered)
            return;
        if (other.gameObject.name == "Player" && this.gameObject.name == "Stackable 0")
            transform.parent = other.transform;
        else
        {
            Vector3 otherContactPt = other.contacts[0].point;

            // True if this collison is being called on the previous top of the stack
            if (otherContactPt.y > this.GetComponent<Collider2D>().bounds.center.y)
            {
                isTop = false;
                return;
            }
            transform.parent = player.transform;
            //Failed 2D joint code that I may need to come back to later
            //FixedJoint2D stackConnection = this.gameObject.AddComponent<FixedJoint2D>();
            //stackConnection.connectedBody = other.rigidbody;
            ////stackConnection.enableCollision = true;
            //stackConnection.dampingRatio = 0.5f;
            //stackConnection.breakForce = 50;
        }
        isFalling = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        stacked = true;
        stack.stackSize++;
        collisionRegistered = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    if (other.name == "Kill Barrier")
        {
            if (stacked)
                stack.stackSize--;
            Destroy(this.gameObject);
            stack.spawnNum--;
            return;
        }
        //Deprecated non physics way
        /*
        isFalling = false;
        other.enabled = false;
        GetComponentsInChildren<Collider2D>()[1].isTrigger = true;
        transform.parent = other.transform;
        */
    }

    public void Drop()
    {
        isFalling = true;
    }

}
