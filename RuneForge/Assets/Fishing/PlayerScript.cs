using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float speed = 1;
    private Vector2 velocity;
    private Vector2 gravity;
    Rigidbody2D player;
    GameObject FishManager;
    FishManager fishScript;

    void Start () {
	    player = this.GetComponent<Rigidbody2D>();
        velocity = new Vector2(0, speed);
        gravity = new Vector2(0, 0.5f);
        FishManager = GameObject.Find("GameManager");
        fishScript = FishManager.GetComponent<FishManager>();


}   

	void FixedUpdate () {
	if (Input.GetKey(KeyCode.Space))
        {
            player.MovePosition(player.position + velocity * Time.deltaTime);
        }
        else
        {
            player.MovePosition(player.position - velocity * Time.deltaTime);
        }
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(fishScript.score);
        fishScript.score += 1;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "fish")
        {
            Physics2D.IgnoreCollision(other.collider, this.GetComponent<Collider2D>());
        }
    }
}
