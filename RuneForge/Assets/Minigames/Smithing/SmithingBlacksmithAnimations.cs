using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithingBlacksmithAnimations : MonoBehaviour {

    private Animator animator;
    private int hammerFrames;
    private bool hammering;

	// Use this for initialization
	void Start ()
    {
        animator = this.GetComponent<Animator>();
        hammerFrames = 24;
        hammering = false;
        
	}

    void Update()
    {
        if (hammering)
        {
            int animationIndex = ((int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * (hammerFrames))) % hammerFrames;
            if (animationIndex == 10)
                Debug.Log("Hammer hit?");
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
    }
}
