using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LaneManager : MonoBehaviour {

    public GameObject player;
    public GameObject[] playerLanes;
    public GameObject[] otherLanes;
    public GameObject[] other;
    public int lane = 1;
    public Text scoreText;
    public Timer timer;
    int score;
    float startTime;
	// Use this for initialization
	void Start () {
        score = 0;
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer.timeEnd)
            GameObject.Find("Canvas").transform.Find("Result").gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.A) && lane > 0)
        {
            lane--;
        }
        if (Input.GetKeyDown(KeyCode.D) && lane < 2)
        {
            lane++;
        }
        player.transform.position = playerLanes[lane].transform.position;

        if(Time.time - startTime > 1)
        {
            startTime = Time.time;
            Instantiate(other[Random.Range(0, other.Length)], otherLanes[Random.Range(0,otherLanes.Length)].transform.position, Quaternion.identity);
        }

        scoreText.text = "Score: " + score;
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if(collide.tag == "Blue")
        {
            Destroy(collide.gameObject);
            score += 50;
        }
        if(collide.tag == "Red")
        {
            Destroy(collide.gameObject);
            score -= 10;
            if (score < 0)
                score = 0;
        }
    }
}
