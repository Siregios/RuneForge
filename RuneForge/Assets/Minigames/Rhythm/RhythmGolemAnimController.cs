using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGolemAnimController : MonoBehaviour {

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (gameObject.name == "w_golem" && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("w_key");
        }
        if (gameObject.name == "s_golem" && Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("s_key");
        }
        if (gameObject.name == "a_golem" && Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("a_key");
        }
        if (gameObject.name == "d_golem" && Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("d_key");
        }
    }

}
