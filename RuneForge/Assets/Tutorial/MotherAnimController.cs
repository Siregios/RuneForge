using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherAnimController : MonoBehaviour {

    Animator anim;
    PlayerController mother; //This is a cheap hack because I am too lazy to write a generic controller class right now
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        mother = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        switch (mother.movingDirection) {
            case Direction.DIRECTION.LEFT:
                anim.SetBool("walk", true);
                spriteRenderer.flipX = false;
                break;
            case Direction.DIRECTION.RIGHT:
                anim.SetBool("walk", true);
                spriteRenderer.flipX = true;
                break;
            case Direction.DIRECTION.NONE:
                anim.SetBool("walk", false);
                break;
        }

    }
}
