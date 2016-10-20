//CREDIT TO https://christianfloisand.wordpress.com/2014/01/23/beat-synchronization-in-unity/ for guide to beat synchronization
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BeatSynchronizer : MonoBehaviour {
    public float bpm = 128f;
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
