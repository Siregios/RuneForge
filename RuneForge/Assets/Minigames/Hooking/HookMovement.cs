using UnityEngine;
using System.Collections;

public class HookMovement : MonoBehaviour {

    public bool aiTag = false;
    public bool boundTag = false;
    public SpriteRenderer visible;
    public GameObject grabbed;


//    GameObject hook;
  //  playerHookScript player;
    void Start () {
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
            grabbed = other.gameObject;
            aiTag = true;
        }
        
        if (other.gameObject.tag == "VerticalBounds" || other.gameObject.tag == "HorizontalBounds")
            boundTag = true;
    }
}
