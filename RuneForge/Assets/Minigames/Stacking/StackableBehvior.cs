using UnityEngine;
using System.Collections;

public class StackableBehvior : MonoBehaviour
{
    public float fallSpeed = 10f;
    //this is set by the manager to be true

    private bool isFalling;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isFalling)
            transform.position -= transform.up * fallSpeed * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Kill Barrier")
        {
            Destroy(this.gameObject);
            return;
        }
        isFalling = false;
        other.enabled = false;
        GetComponentsInChildren<Collider2D>()[1].isTrigger = true;
        transform.parent = other.transform;
    }

    public void Drop()
    {
        isFalling = true;
    }
}
