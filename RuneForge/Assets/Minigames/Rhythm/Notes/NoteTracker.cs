using UnityEngine;
using System.Collections;

public class NoteTracker : MonoBehaviour {
    public int accuracy = 0;
	
	void Start () {
        if (transform.position == new Vector3(0, 6, 0))
        {            
        }
	}
	
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("center").transform.position, 2 * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Note")
            accuracy++;
        if (accuracy == 3)
            Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.W) && other.gameObject.name == "w_key")
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.S) && other.gameObject.name == "s_key")
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.A) && other.gameObject.name == "a_key")
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.D) && other.gameObject.name == "d_key")
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
    }
}
