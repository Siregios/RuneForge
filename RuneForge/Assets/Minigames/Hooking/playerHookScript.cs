using UnityEngine;
using System.Collections;

public class playerHookScript : MonoBehaviour {
    //Gets the transform of the hook
    Transform hookTransform;
    
    //Check if the hook has already been released and alternate the hook rotation
    private bool alternate = true;
    private bool hookOut = false;

    //Rotation values for the actual gameobject and the raycast
    float rotationZ;
    //float eulerZ;

    //Transform position of the hook
    private Vector3 hookPos;

    //takes the hook gameobject and script to control in this script (visibility, movement)
    GameObject hook;
    GameObject aiGrab;
    HookMovement hookMovement;
    SpriteRenderer visible;
    float hookSpeed = 7f;

    //Decrements HookManager
    GameObject hookManager;
    HookManager decrement;

    void Start () {
	    hookTransform = GetComponent<Transform>();
        hookPos = this.transform.position;
        hook = GameObject.Find("hookMove");
        hookMovement = hook.GetComponent<HookMovement>();
        visible = hook.GetComponent<SpriteRenderer>();
        hookManager = GameObject.Find("GameManager");
        decrement = hookManager.GetComponent<HookManager>();

    }

	void FixedUpdate () {
        //Checks when it hits a collider
        if (hookMovement.aiTag || hookMovement.boundTag)
        {
            //If the hook returns to original position, change all variables back to false oh god
            if (Vector3.Distance(hook.transform.position, hookPos) < 0.2f)
            {
                hookOut = false;                
                
                visible.enabled = false;
                hook.transform.position = hookPos;
                hook.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                hookMovement.boundTag = false;
                hook.GetComponent<Collider2D>().enabled = true;
                if (hookMovement.aiTag)
                {
                    hookMovement.aiTag = false;
                    Destroy(hookMovement.grabbed);
                    decrement.currentObjects--;
                }
            }
            //Otherwise keep making the hook move man
            else
            {
                hook.GetComponent<Collider2D>().enabled = false;
                hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookPos, hookSpeed * 2 * Time.deltaTime);
                if (hookMovement.aiTag)
                {
                    Vector3 aiPos = hookMovement.grabbed.GetComponent<Transform>().transform.position;
                    hookMovement.grabbed.GetComponent<Transform>().transform.position = Vector3.MoveTowards(aiPos, hook.transform.position, hookSpeed * 3 * Time.deltaTime);
                    hookMovement.grabbed.GetComponent<BoxCollider2D>().enabled = false;
                }
                
            }
        }

        //If hook hasn't hit something, make it move that direction
        else if (hookOut)
        {
            hook.GetComponent<Rigidbody2D>().velocity = transform.up * hookSpeed;
        }

        //If inputted and hook isn't out already, THEN GO MY HOOK THAT I WILL CALL EDDIE WIN
        else if (Input.GetKeyDown(KeyCode.Space) && hookOut == false)
        {
            hookOut = true;
            visible.enabled = true;
            hook.transform.rotation = this.transform.rotation;

        }
        //Otherwise keep rotating the controlling thing
        else if (hookOut == false)
        {
            rotationZ = hookTransform.rotation.z;
            //eulerZ = hookTransform.rotation.eulerAngles.z;
            checkDirection();
        }

        
	}

    //rotates player
    void checkDirection()
    {
        if (alternate)
        {
            transform.Rotate(Vector3.forward);
            if (rotationZ >= .55)
                alternate = false;
        }

        if (!alternate)
        {
            transform.Rotate(-Vector3.forward);
            if (rotationZ <= -.55)
                alternate = true;
        }

    }
}
