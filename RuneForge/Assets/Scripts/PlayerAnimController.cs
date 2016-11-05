using UnityEngine;
using System.Collections;

public class PlayerAnimController : MonoBehaviour {

    Animator anim;
    PlayerController player;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = transform.parent.gameObject.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        //Edwin holy moly
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

        if (player.mouseClick)
        {
            anim.SetBool("walk", true);
            if (player.playerPosition.x - player.targetClick.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || (!player.mouseClick && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
        {
            anim.SetBool("walk", false);
        }
    }
}
