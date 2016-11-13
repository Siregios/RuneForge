using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour {

    TracerManager manager;
    GameObject hitCircle;
    float lifeTime = 0f;
    float timeRemaining = 0f;

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<TracerManager>();
        hitCircle = this.transform.FindChild("Circle").gameObject;
    }

    void Start()
    {
        lifeTime = timeRemaining = manager.spawnInterval;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        float percentageSize = timeRemaining / lifeTime;
        hitCircle.transform.localScale = new Vector3(percentageSize, percentageSize, 1);

        if (timeRemaining <= 0)
        {
            manager.DotMissed(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    void OnMouseEnter()
    {
        if (!this.enabled)
            return;
        manager.DotTouched(this.gameObject);
        Destroy(this.gameObject);
    }
}
