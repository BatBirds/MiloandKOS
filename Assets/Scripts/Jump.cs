﻿using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    Animator anim;
    int jumpingTrigger;
    int groundedBool;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        jumpingTrigger = Animator.StringToHash("Jumping");
        groundedBool = Animator.StringToHash("Grounded");
    }
	
    void FixedUpdate()
    {
        // Jumping.
        if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool(groundedBool))
        {
            anim.SetTrigger(jumpingTrigger);
            anim.SetBool(groundedBool, false);
            rigidbody.velocity = new Vector3(0.0f, 3.5f, 0.0f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        anim.SetBool(groundedBool, true);
    }
}