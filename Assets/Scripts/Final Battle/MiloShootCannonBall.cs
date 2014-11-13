﻿using UnityEngine;
using System.Collections;

public class MiloShootCannonBall : MonoBehaviour
{
    public GameObject rotatingCanonBallPrefab;
    Animator anim;
    int shootCannonBall;
    GameObject milo;
    Vector2 vMeasures = new Vector2(1.4f, 0.0f);//DON'T MESS WITH THESE NUMBERS!

    void Awake()
    {
        anim = GetComponent<Animator>();
        shootCannonBall = Animator.StringToHash("ShootCannonBall");
    }
    // Use this for initialization
    void Start()
    {
        milo = GameObject.Find("MiloCannon01");
    }
    
    // Update is called once per frame
    void Update()
    {
        /*
         * Checking for scene as well because otherwise, KOS will shoot when Milo shoots.
         */
        if (Input.GetKeyDown(KeyCode.C) && Application.loadedLevelName.Equals("OutroCutsceneMilo"))
        {
            anim.SetTrigger(shootCannonBall);
            StartCoroutine("SpawnCannonball");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "RotatingAxe01")
        {
            Destroy(col.gameObject);
        }
    }

    /// <summary>
    /// Spawns the cannonball.
    /// </summary>
    /// <returns>The cannonball.</returns>
    public IEnumerator SpawnCannonball()
    {
        yield return new WaitForSeconds(0.6f);//wait until the animation is done playing, then throw the axe.
        GameObject cannonBall = Instantiate(rotatingCanonBallPrefab, new Vector2(milo.transform.position.x - vMeasures.x, milo.transform.position.y - vMeasures.y), Quaternion.identity) as GameObject;
        cannonBall.rigidbody2D.AddForce(Vector2.up * 100 + Vector2.right * -800, ForceMode2D.Force);

    }
}
