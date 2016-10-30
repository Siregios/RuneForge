using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {

    public GameObject projectile;
    public FishManager manager;
    float start;
    public int t1 = 5;
    public int t2 = 30;
    int timer;
    int timeRemaining;
	// Use this for initialization
	void Start () {
        start = Time.time;
        timer = t2;
	}
	

    void FixedUpdate()
    {
        timeRemaining = (int)Mathf.Round((timer - (Time.time - start)));
        manager.timer.text = timeRemaining.ToString();
        if(timeRemaining <= 0)
        {
            //End Game
            Destroy(gameObject);
        }
        if(Time.time - start > Random.Range(t1, t2))
        {
            Instantiate(projectile, Camera.main.ViewportToWorldPoint(new Vector3(Mathf.Round(Random.value), Random.value, 0f)), Quaternion.identity);
            t1++;
            t2++;
        }
    }
}
