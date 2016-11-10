//CREDIT TO https://christianfloisand.wordpress.com/2014/01/23/beat-synchronization-in-unity/ for guide to beat synchronization
using UnityEngine;
using System.Collections;

/// <summary>
/// This class should be attached to the audio source for which synchronization should occur, and is 
/// responsible for synching up the beginning of the audio clip with all active beat counters and pattern counters.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BeatSynchronizer : MonoBehaviour
{
    public float bpm = 145f;
    public float startDelay;
    public delegate void AudioStartAction(double syncTime);
    public static event AudioStartAction OnAudioStart;
    bool audioPlaying = false;
    public GameObject CenterMap;
    private bool mapping = false;

    // Use this for initialization
    void Start()
    {
        if (CenterMap != null)
        {
            double initTime = AudioSettings.dspTime;
            GetComponent<AudioSource>().PlayScheduled(initTime + startDelay);
            if (OnAudioStart != null)
            {
                OnAudioStart(initTime + startDelay);
            }
            audioPlaying = true;
            mapping = true;
        }
        //GetComponent<AudioSource>().Stop();
        //if (OnAudioStart != null)
        //    OnAudioStart(initTime + startDelay);
        //GetComponent<AudioSource>().PlayDelayed(startDelay);
    }

    void Update()
    {
        if (!mapping)
        {
            if (GameObject.Find("RandomSpawn").GetComponent<RandomSpawnNote>().songStart && !audioPlaying)
            {
                double initTime = AudioSettings.dspTime;
                GetComponent<AudioSource>().PlayScheduled(initTime + startDelay);
                if (OnAudioStart != null)
                {
                    OnAudioStart(initTime + startDelay);
                }
                audioPlaying = true;
            }
        }
    }
}