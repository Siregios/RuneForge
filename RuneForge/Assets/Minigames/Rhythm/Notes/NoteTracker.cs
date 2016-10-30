using UnityEngine;
using System.Collections;

public class NoteTracker : MonoBehaviour {
    public int accuracy = 0;
    public float speed = 3.5f;

	void Start () {
        if (transform.position == new Vector3(0, 6, 0))
        {            
        }
	}
	
	
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("center").transform.position, speed * Time.deltaTime);
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
        if (Input.GetKey(KeyCode.W) && other.gameObject.name == "w_key" && accuracy != 3)
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
        else if (Input.GetKey(KeyCode.S) && other.gameObject.name == "s_key" && accuracy != 3)
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
        else if (Input.GetKey(KeyCode.A) && other.gameObject.name == "a_key" && accuracy != 3)
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
        else if (Input.GetKey(KeyCode.D) && other.gameObject.name == "d_key" && accuracy != 3)
        {
            Debug.Log(accuracy);
            Destroy(gameObject);
        }
    }
}
