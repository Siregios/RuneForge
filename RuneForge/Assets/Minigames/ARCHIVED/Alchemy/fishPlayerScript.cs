using UnityEngine;
using System.Collections;

public class fishPlayerScript : MonoBehaviour {

    //Controls player speed and takes script of fish manager to accumulate score.
    public float speed = 1;
    public int damage;
    //private Vector2 velocity;    
    Rigidbody2D player;
    GameObject FishManager;
    public GameObject target;
    FishManager fishScript;
    public Score score;

    void Start () {
	    player = this.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.GetComponent<Collider2D>());
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
            score.addScore(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        score.addScore(1);
    }
}
