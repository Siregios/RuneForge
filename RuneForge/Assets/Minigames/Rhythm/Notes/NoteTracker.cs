﻿using UnityEngine;
using System.Collections;

public class NoteTracker : MonoBehaviour {
    public int accuracy = 0;

	void Start () {
        if (transform.position == new Vector3(0, 6, 0))
        {            
        }
	}
	
	
	void Update () {   
        transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("center").transform.position, 3.5f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Note" && other.gameObject.name != "hitbox")
            accuracy++;
        if (accuracy == 3)
        {
            Debug.Log("Miss!");
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.W) && other.gameObject.name == "w_key" && accuracy != 3)
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.S) && other.gameObject.name == "s_key" && accuracy != 3)
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.A) && other.gameObject.name == "a_key" && accuracy != 3)
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.D) && other.gameObject.name == "d_key" && accuracy != 3)
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
    }
}
