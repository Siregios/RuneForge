using UnityEngine;
using System.Collections;
using SynchronizerData;

public class RandomSpawnNote : MonoBehaviour {
    public GameObject[] spawnObject;
    int randomInt = 1;
    private BeatObserver beatObserver;
    private int beatCounter;

    void Start () {
        beatObserver = GetComponent<BeatObserver>();
        beatCounter = 0;
        Instantiate(spawnObject[0], spawnObject[0].transform.position, Quaternion.identity);
    }
		
	void Update () {
        if (beatObserver.beatMask == BeatType.OnBeat)
        {
            randomInt = Random.Range(1, 5);
            if (randomInt == 1)            
                Instantiate(spawnObject[0], spawnObject[0].transform.position, Quaternion.identity);                
            else if (randomInt == 2)
                Instantiate(spawnObject[1], spawnObject[1].transform.position, Quaternion.identity);
            else if (randomInt == 3)
                Instantiate(spawnObject[2], spawnObject[2].transform.position, Quaternion.identity);
            else if (randomInt == 4)
                Instantiate(spawnObject[3], spawnObject[3].transform.position, Quaternion.identity);
        }
	}
}
