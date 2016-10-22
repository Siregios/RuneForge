//CREDIT TO https://christianfloisand.wordpress.com/2014/01/23/beat-synchronization-in-unity/ for guide to beat synchronization
using UnityEngine;
using System.Collections;

/// <summary>
/// This class should be attached to the audio source for which synchronization should occur, and is 
/// responsible for synching up the beginning of the audio clip with all active beat counters and pattern counters.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BeatSynchronizer : MonoBehaviour {
    public float bpm = 145f;
    public float startDelay = 1f;
    public delegate void AudioStartAction(double syncTime);
    public static event AudioStartAction OnAudioStart;

	// Use this for initialization
	void Start () {
        double initTime = AudioSettings.dspTime;
        GetComponent<AudioSource>().PlayScheduled(initTime + startDelay);
        if (OnAudioStart != null)
            OnAudioStart(initTime + startDelay);
	}
}
