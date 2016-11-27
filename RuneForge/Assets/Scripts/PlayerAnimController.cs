using UnityEngine;
using System.Collections;

public class PlayerAnimController : MonoBehaviour {

    Animator anim;
    PlayerController player;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<PlayerController>();
        boxCollider = GetComponentInParent<BoxCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        

        //if (Input.GetKey(KeyCode.A))
        //{
        //    anim.SetBool("walk", true);
        //    GetComponent<SpriteRenderer>().flipX = false;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    anim.SetBool("walk", true);
        //    GetComponent<SpriteRenderer>().flipX = true;
        //}

        switch (player.movingDirection)
        {
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

        if (spriteRenderer.flipX)
            boxCollider.offset = new Vector2(Mathf.Abs(boxCollider.offset.x), boxCollider.offset.y);
        else
            boxCollider.offset = new Vector2(-1 * Mathf.Abs(boxCollider.offset.x), boxCollider.offset.y);
        //if (player.mouseClick && (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
        //{
        //    anim.SetBool("walk", true);
        //    if (player.playerPosition.x - player.targetClick.x < 0)
        //        GetComponent<SpriteRenderer>().flipX = true;
        //    else
        //        GetComponent<SpriteRenderer>().flipX = false;
        //}

        //If any key is lifted or you reached destination or the player is stuck at a vertical wall, walk animation off.
        //if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || (!player.mouseClick && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || player.bound)
        //{
        //    anim.SetBool("walk", false);
        //}
    }
}
