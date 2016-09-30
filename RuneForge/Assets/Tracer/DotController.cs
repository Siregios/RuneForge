using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour {

    TracerManager manager;
    GameObject hitCircle;
    float timeRemaining = 0f;

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<TracerManager>();
        hitCircle = this.transform.FindChild("Circle").gameObject;
    }

    void Start()
    {
        timeRemaining = manager.spawnInterval;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        float percentageSize = timeRemaining / manager.spawnInterval;
        hitCircle.transform.localScale = new Vector3(percentageSize, percentageSize, 1);
    }

    void OnMouseEnter()
    {
        manager.DotTouched(this.gameObject);
        Destroy(this.gameObject);
    }
}
