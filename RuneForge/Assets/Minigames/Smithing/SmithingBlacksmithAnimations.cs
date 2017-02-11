using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingBlacksmithAnimations : MonoBehaviour {

    private Animator animator;
    private AudioManager AudioManager;
    private int hammerFrames;
    private bool hammering;
    private bool reset;
    public bool hit;

	// Use this for initialization
	void Start ()
    {
        animator = this.GetComponent<Animator>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        hammerFrames = 24;
        hammering = false;
        hit = true;

        //Reset is true if the animation has reset
        //This was necessary because due to the nature of animations and rounding
        //You will sometimnes get instance where the same frame has said to occured twice
        reset = true;
        
	}

    void Update()
    {
        if (hammering)
        {
            int animationIndex = ((int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * (hammerFrames))) % hammerFrames;
            if (animationIndex <= 3)
                reset = true;
            if (animationIndex >= 10  && reset)
            {
                //Debug.Log("Hammer hit?");
                if (hit)
                    AudioManager.PlaySound(0);
                else
                    AudioManager.PlaySound(1);
                reset = false;
            }
        }
    }

    public void startHammering()
    {
        animator.SetBool("Hammering", true);
        hammering = true;
    }

    public void stopHammering()
    {
        animator.SetBool("Hammering", false);
        hammering = false;
        reset = true;
        //resetSpeed();
    }

    public void increaseSpeed()
    {
        animator.speed += .1f;
    }

    public void resetSpeed()
    {
        animator.speed = 1f;
    }
}
