using UnityEngine;
using System.Collections;

public class StackableBehavior : MonoBehaviour
{
    public float fallSpeed = 10f;
    //this is set by the manager to be true

    private bool isFalling;
    protected bool isTop;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        isTop = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isFalling)
            transform.position -= transform.up * fallSpeed * Time.deltaTime;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isTop)
            return;
        if (other.gameObject.name == "Player" && this.gameObject.name == "Stackable 0")
            transform.parent = other.transform;
        else
        {
            transform.parent = player.transform;
        }
        isFalling = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    if (other.name == "Kill Barrier")
        {
            Destroy(this.gameObject);
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
