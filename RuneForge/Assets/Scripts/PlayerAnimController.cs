using UnityEngine;
using System.Collections;

public class PlayerAnimController : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<SpriteRenderer>().flipX)
            GetComponentInParent<BoxCollider2D>().offset = new Vector2(Mathf.Abs(GetComponentInParent<BoxCollider2D>().offset.x) , GetComponentInParent<BoxCollider2D>().offset.y);
        else
            GetComponentInParent<BoxCollider2D>().offset = new Vector2(-1 * Mathf.Abs(GetComponentInParent<BoxCollider2D>().offset.x), GetComponentInParent<BoxCollider2D>().offset.y);
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("walk", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("walk", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("walk", false);
        }
    }
}
