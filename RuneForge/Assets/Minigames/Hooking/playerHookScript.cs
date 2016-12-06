﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHookScript : MonoBehaviour {
    //Gets the transform of the hook
    Transform hookTransform;
    private Vector3 hookPos;

    //Check if the hook has already been released and alternate the hook rotation
    private bool alternate = true;
    private bool hookOut = false;

    //Rotation values for the actual gameobject and the raycast
    float rotationZ;
    //float eulerZ;

    //takes the hook gameobject and script to control in this script (visibility, movement)
    GameObject hook;
    HookMovement hookMovement;
    SpriteRenderer visible;
    public float hookSpeed = 10f;

    //Decrements HookManager
    HookManager decrement;
    public Timer timer;
    public float timerTime;

    private AudioManager AudioManager;

    void Start () {
        timerTime = timer.time;
	    hookTransform = GetComponent<Transform>();
        hookPos = this.transform.position;
        hook = GameObject.Find("hookMove");
        hookMovement = hook.GetComponent<HookMovement>();
        visible = hook.GetComponent<SpriteRenderer>();
        decrement = GameObject.Find("GameManager").GetComponent<HookManager>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    void Update()
    {
        //if (!hookOut && decrement.remainingHooks > 0)
        //{
        //    timer.stopTimer = false;
        //}

        //If inputted and hook isn't out already, THEN GO MY HOOK THAT I WILL CALL EDDIE WIN
        if (((Input.GetKeyDown(KeyCode.Space) || timer.timeEnd) && hookOut == false) && decrement.remainingHooks > 0)
        {
            hookOut = true;
            AudioManager.PlaySound(0);
            visible.enabled = true;
            timer.stopTimer = true;
            hook.transform.rotation = this.transform.rotation;
            decrement.remainingHooks--;
        }
    }
	void FixedUpdate () {
        //Checks when it hits boundary collider
        if (hookMovement.boundTag)
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
                timer.time = timerTime;
                timer.stopTimer = false;
                timer.timeEnd = false;
                if (decrement.remainingHooks == 0)
                {
                    decrement.endGame = true;
                }
                if (hookMovement.aiTag)
                {
                    hookMovement.aiTag = false;
                    foreach (GameObject obj in hookMovement.grabbed)
                    {
                        Destroy(obj);
                        decrement.currentObjects--;
                        if (obj.name == "bad(Clone)")
                            decrement.neg--;
                    }
                    hookMovement.grabbed.Clear();
                }
            }
            //Otherwise keep making the hook move man
            else
            {
                hook.GetComponent<Collider2D>().enabled = false;
                hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookPos, hookSpeed * 2 * Time.deltaTime);
                foreach (GameObject obj in hookMovement.grabbed)
                {
                    obj.transform.position = Vector3.MoveTowards(obj.transform.position, hook.transform.position, hookSpeed * 3 * Time.deltaTime);
                }
                //if (hookMovement.aiTag)
                //{
                //    Vector3 aiPos = hookMovement.grabbed.GetComponent<Transform>().transform.position;
                //    hookMovement.grabbed.GetComponent<Transform>().transform.position = Vector3.MoveTowards(aiPos, hook.transform.position, hookSpeed * 3 * Time.deltaTime);
                //    hookMovement.grabbed.GetComponent<BoxCollider2D>().enabled = false;
                //}

            }
        }

        //If hook hasn't hit something, make it move that direction
        else if (hookOut)
        {
            foreach (GameObject obj in hookMovement.grabbed)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, hook.transform.position, hookSpeed * 3 * Time.deltaTime);
            }
            hook.GetComponent<Rigidbody2D>().velocity = transform.up * hookSpeed;
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
            transform.Rotate(Vector3.forward * 1.5f);
            if (rotationZ >= .55)
                alternate = false;
        }

        if (!alternate)
        {
            transform.Rotate(-Vector3.forward * 1.5f);
            if (rotationZ <= -.55)
                alternate = true;
        }

    }
}
