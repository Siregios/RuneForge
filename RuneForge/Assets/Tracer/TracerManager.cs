using UnityEngine;
using System.Collections;

public class TracerManager : MonoBehaviour {

    RandomSpawner spawner;

    void Awake()
    {
        spawner = this.GetComponent<RandomSpawner>();
    }
    
    public void SpawnDot()
    {
        spawner.SpawnObject();
    }
}
