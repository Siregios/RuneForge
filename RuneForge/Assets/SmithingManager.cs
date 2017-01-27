using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingManager : MonoBehaviour {

    private AudioManager AudioManager;
    private AudioSource music;

    void Awake()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        music = this.gameObject.GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        music.Play();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
