using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXBank : MonoBehaviour {

    public AudioClip[] SFX;
    public Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < SFX.Length; i++)
        {
            sfxDict[SFX[i].name] = SFX[i];
        }
    }

    //Note only use this for when a sound must be called from something that is not part of a class
    //Otherwise the sound should be played from the class itself
    //O(n) search for string in dictionary vs. O(1) playing an assigned sound in a class
    public void playSFX(string AudioClipStr)
    {
        MasterGameManager.instance.audioManager.PlaySFXClip(sfxDict[AudioClipStr]);
    }

}
