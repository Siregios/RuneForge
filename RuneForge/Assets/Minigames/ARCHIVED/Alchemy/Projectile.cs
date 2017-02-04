using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
	    if(Camera.main.WorldToViewportPoint(transform.position).x > .5)
        {
            speed = -speed;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1.1 || Camera.main.WorldToViewportPoint(transform.position).x < -0.1)
            Destroy(gameObject);
    }
}
