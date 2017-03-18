using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingManager : MonoBehaviour {

    private AudioManager AudioManager;

    void Awake()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
}
