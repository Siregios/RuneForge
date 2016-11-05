using UnityEngine;
using System.Collections;

public class PlayerAnimController : MonoBehaviour {

    Animator anim;
    PlayerController player;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = GetComponentInParent<PlayerController>();
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
        if (player.mouseClick && (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
        {
            anim.SetBool("walk", true);
            if (player.playerPosition.x - player.targetClick.x < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX = false;
        }

        //If any key is lifted or you reached destination or the player is stuck at a vertical wall, walk animation off.
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || (!player.mouseClick && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || player.bound)
        {
            anim.SetBool("walk", false);
        }
    }
}
