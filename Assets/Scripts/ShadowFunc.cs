﻿using UnityEngine;
using System.Collections;

public class ShadowFunc : MonoBehaviour
{

    private Animator animator;
    private float chaseValue = 1.5f;
    GameObject milo; 

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        milo = GameObject.Find("Milo");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Milo" && GameController.INSTANCE.IsPlayingAsMilo)//Only chase Milo when he is playing.
        {
            InvokeRepeating("ChaseMilo", 0f, 0.03f);
            StartCoroutine("ShadowTimer");
            InvokeRepeating("ShadowMovement", 0f, 0.1f);
        }
    }
	

    // Update is called once per frame
    void Update()
    {
        Quaternion newRotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
        newRotation.x = 0;
        newRotation.z = 0; 
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8); 
    }


    IEnumerator ShadowTimer()
    {
        yield return new WaitForSeconds(10);
        RemoveShadow();
    }

    void RemoveShadow()
    {
        gameObject.SetActive(false);
    }


    void ShadowMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 && vertical == 0)
        {
            if (horizontal == 1)
            {
                // animator.SetBool("ShadowIdle", false);
                animator.SetBool("ShadowRunningFront", true);
                // animator.SetBool("ShadowRunningSidewayRight", false);
                // animator.SetBool("ShadowRunningSidewayLeft", false);

            } else if (horizontal == -1)
            {
                // animator.SetBool("ShadowIdle", false);
                animator.SetBool("ShadowRunningFront", true);
                // animator.SetBool("ShadowRunningSidewayRight", false);
                // animator.SetBool("ShadowRunningSidewayLeft", false);
            } 
        } else if (vertical != 0 && horizontal == 0)
        {
            // animator.SetBool("ShadowIdle", false);
            animator.SetBool("ShadowRunningFront", true);
            // animator.SetBool("ShadowRunningSidewayRight", false);
            // animator.SetBool("ShadowRunningSidewayLeft", false);
        }
    }


    void ChaseMilo()
    {
        Vector3 dir = milo.transform.position - transform.position;
        dir.y = 0.0f;
        Vector3 velocity = dir.normalized * chaseValue;
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }
}
