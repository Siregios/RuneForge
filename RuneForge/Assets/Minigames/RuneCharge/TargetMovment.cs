using UnityEngine;
using System.Collections;

public class TargetMovment : MonoBehaviour {

    public float leftBound = -6.0f;
    public float rightBound = 6.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changePosition()
    {
        transform.position = new Vector2(Random.Range(leftBound, rightBound), transform.position.y);
    }

    public float getCenterX()
    {
        return transform.position.x;
    }
}
