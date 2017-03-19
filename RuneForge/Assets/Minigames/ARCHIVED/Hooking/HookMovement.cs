using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HookMovement : MonoBehaviour {

    HookManager addScore;
    public Score score;
    public AudioClip[] scoreSounds;
    [HideInInspector]
    public List<GameObject> grabbed;
    [HideInInspector]
    public bool aiTag, boundTag = false;
    [HideInInspector]
    public SpriteRenderer visible;

    //    GameObject hook;
    //  playerHookScript player;
    void Start () {
        addScore = GameObject.Find("GameManager").GetComponent<HookManager>();
        visible.enabled = false;
        Physics2D.IgnoreLayerCollision(8, 2);
        //    hook = GameObject.Find("hook");
        //  player = hook.GetComponent<playerHookScript>();
    }
	    
	void Update () {        
            
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "AI")
        {
            grabbed.Add(other.gameObject);            
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            if (other.gameObject.name == "good(Clone)")
            {
                score.addScore(20);
                MasterGameManager.instance.audioManager.PlaySFXClip(scoreSounds[1]);
            }
            else if (other.gameObject.name == "great(Clone)")
            {
                score.addScore(100);
                MasterGameManager.instance.audioManager.PlaySFXClip(scoreSounds[0]);
            }
            else if (other.gameObject.name == "bad(Clone)")
            {
                score.subScore(40);
                MasterGameManager.instance.audioManager.PlaySFXClip(scoreSounds[2]);
            }
            aiTag = true;
        }
        
        if (other.gameObject.tag == "VerticalBounds" || other.gameObject.tag == "HorizontalBounds")
            boundTag = true;
    }
}
