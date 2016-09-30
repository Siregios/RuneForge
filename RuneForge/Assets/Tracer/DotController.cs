using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour {

    TracerManager manager;

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<TracerManager>();
    }

    void OnMouseEnter()
    {
        Destroy(this.gameObject);
        manager.SpawnDot();
    }
}
