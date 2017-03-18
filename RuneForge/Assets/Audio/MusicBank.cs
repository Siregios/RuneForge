using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBank : MonoBehaviour {
    //Song to play on startup
    public AudioClip startSong;

    //Any other additional songs to play
    public AudioClip[] additionalSongs;


	// Use this for initialization
	void Start ()
    {
        if (startSong)
            MasterGameManager.instance.audioManager.PlayMusic(startSong);
    }
}
