using UnityEngine;
using System.Collections;

public class FishAI : MonoBehaviour {

    public float speed = 1;
    private float pause = 20;
    private float lastPause;
    Vector3 pos;
    int rand = 0;


	// Use this for initialization
	void Start () {
        lastPause = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        rand = Random.Range(0, 2);
        if (rand == 0 && lastPause > Time.time)
        {
            MovementAI();
        }
        else
        {
            lastPause = Time.time + pause;
        }
    }

    private void MovementAI()
    {
        transform.position += transform.up * speed * Time.deltaTime;
      
    }

}
