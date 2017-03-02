using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingManager : MonoBehaviour {

    private AudioManager AudioManager;
    private GameObject AudioManagerObject;

    void Awake()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        AudioManagerObject = GameObject.Find("AudioManager");
    }

    // Use this for initialization
    void Start ()
    {
       AudioManagerObject.GetComponent<AudioSource>().Play();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
