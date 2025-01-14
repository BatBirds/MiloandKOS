﻿using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    Animator anim;
    int movementFloat;
//    public static float verticalMovement;
    float verticalMovement;
    float shadowSlowDownSpeed = 1;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movementFloat = Animator.StringToHash("Movement");
    }

    void FixedUpdate()
    {
        if (!SwitchFadingInOut.SwitchStarting)//Prevent movements during switch phases.
        {
            MovementControl();
        }
    }

    /// <summary>
    /// Gets or sets the shadow slow down speed.
    /// </summary>
    /// <value>The shadow slow down speed.</value>
    public float ShadowSlowDownSpeed
    {
        get
        {
            return shadowSlowDownSpeed;
        }
        set
        {
            shadowSlowDownSpeed = value;
        }
    }

    /// <summary>
    /// Control the movements.; Walk, Run inputs and the appropriate animation states
    /// </summary>
    void MovementControl()
    {
        //Left Rotation.
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) )
        {
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Horizontal") - 2.0f, 0.0f));
        }
        //& Right Rotation
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Horizontal") + 2.0f, 0.0f));
        }
        //Forward direction.
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            verticalMovement = Input.GetAxis("Vertical") * 2.5f * shadowSlowDownSpeed;
            anim.SetFloat(movementFloat, verticalMovement);
            transform.Translate(0.0f, 0.0f, verticalMovement * Time.deltaTime);
        }
        //Backward direction.
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            verticalMovement = Input.GetAxis("Vertical") * shadowSlowDownSpeed;
            anim.SetFloat(movementFloat, verticalMovement);
            transform.Translate(0.0f, 0.0f, -Vector3.back.z * (verticalMovement * Time.deltaTime));
        }
        //When NO keyboard events are present.
        if (!Input.anyKey)
        {
            anim.SetFloat(movementFloat, 0.0f);
        }
    }
}
  