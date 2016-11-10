using UnityEngine;
using System.Collections;
using SynchronizerData;

public class CenterBehavior : MonoBehaviour {

    private BeatObserver beatObserver;
    private int beatCounter;


    void Start()
    {
        beatObserver = GetComponent<BeatObserver>();
        beatCounter = 0;
    }

    void Update()
    {
        if ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)
        {
            //This will check for quarter beats for now, can add animations or some sort to the rune in the middle here!
        }
    }
}
