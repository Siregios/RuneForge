using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {

    public GameObject projectile;
    public FishManager manager;
    float start;
    //when to start and end spawning
    public int t1 = 5;
    public int t2 = 30;
    int timer;
    public float timeRemaining;

	void Start () {
        start = Time.time;
        timeRemaining = t2;
	}
	
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        manager.timer.text = "Time Left: " + Mathf.FloorToInt(timeRemaining).ToString();
    }
    void FixedUpdate()
    {
        
        if(Time.time - start > Random.Range(t1, t2))
        {
            Instantiate(projectile, Camera.main.ViewportToWorldPoint(new Vector3(Mathf.Round(Random.value), Random.value, 0f)), Quaternion.identity);
            t1++;
            t2++;
        }
    }
}
