using UnityEngine;
using System.Collections;

public class fishPlayerScript : MonoBehaviour {

    //Controls player speed and takes script of fish manager to accumulate score.
    public float speed = 1;
    public int damage;
    //private Vector2 velocity;    
    Rigidbody2D player;
    GameObject FishManager;
    FishManager fishScript;

    void Start () {
	    player = this.GetComponent<Rigidbody2D>();
        //velocity = new Vector2(0, speed);
        FishManager = GameObject.Find("GameManager");
        fishScript = FishManager.GetComponent<FishManager>();


}   

	void FixedUpdate () {
	if (Input.GetKey(KeyCode.Space))
        {
            //player.MovePosition(player.position + velocity * Time.deltaTime);
            player.AddForce(new Vector2(0, 20f));
        }
        else
        {
            //player.MovePosition(player.position - velocity * Time.deltaTime);
        }
	}

    //As long as the player is hitting the fish, add score.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
            fishScript.score -= damage;
            if (fishScript.score < 0)
                fishScript.score = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        fishScript.score += 1;
    }

    //Ignore the fish's rigidbody2d.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "AI")
        {
            Physics2D.IgnoreCollision(other.collider, this.GetComponent<Collider2D>());
        }
    }
}
