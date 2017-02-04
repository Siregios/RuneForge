using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HookMovement : MonoBehaviour {

    HookManager addScore;
    public Score score;
    [HideInInspector]
    public List<GameObject> grabbed;
    [HideInInspector]
    public bool aiTag, boundTag = false;
    [HideInInspector]
    public SpriteRenderer visible;

    private AudioManager AudioManager;

    //    GameObject hook;
    //  playerHookScript player;
    void Start () {
        addScore = GameObject.Find("GameManager").GetComponent<HookManager>();
        visible.enabled = false;
        Physics2D.IgnoreLayerCollision(8, 2);
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
                AudioManager.PlaySound(2);
            }
            else if (other.gameObject.name == "great(Clone)")
            {
                score.addScore(100);
                AudioManager.PlaySound(3);
            }
            else if (other.gameObject.name == "bad(Clone)")
            {
                score.subScore(40);
                AudioManager.PlaySound(1);
            }
            aiTag = true;
        }
        
        if (other.gameObject.tag == "VerticalBounds" || other.gameObject.tag == "HorizontalBounds")
            boundTag = true;
    }
}
