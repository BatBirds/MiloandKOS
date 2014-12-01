﻿using UnityEngine;
using System.Collections;

public class KOSThrowAxe : MonoBehaviour
{
    public GameObject rotatingAxePrefab;
    Animator anim;
    int axeThrow;
    GameObject kos;
    GameObject axe;
    GameObject aimingArm;
    bool cooldown = false;
    float angel = 300;
    Vector3 curRot;
    float maxAim = 20.0f;
    // float minAim = -20.0f;
    float aimSpeed = 3.0f;
    float maxZ;
    float minZ;
    float MaxAngel = 500;
    float MinAngel = 100;
    Vector3 armAngel = new Vector3(0f, 0f, 1f); 
    Vector2 vMeasures = new Vector2(1.3f, 0.6f);//DON'T MESS WITH THESE NUMBERS!
    //Vector2 vMeasures = new Vector2(0.6f, 0.3f);//DON'T MESS WITH THESE NUMBERS!

    void Awake()
    {
        anim = GetComponent<Animator>();
        axeThrow = Animator.StringToHash("AxeThrow");
    }

    // Use this for initialization
    void Start()
    {
        kos = GameObject.Find("KOSDoubleAxe01");
        aimingArm = GameObject.Find("KOSAimingArm");
        curRot = aimingArm.transform.eulerAngles;
        maxZ = curRot.z + maxAim;
        minZ = curRot.z - maxAim;
    }

    // Update is called once per frame
    void Update()
    {
        if (StateController.INSTANCE.HasDialoguesBeenStarted)//Ensures we can't attack before both dialogues are done.
        {
            // Checking for scene as well because otherwise, Milo will shoot when Kos shoots.
            if (Input.GetKeyDown(KeyCode.Space) && Application.loadedLevelName.Equals(StateController.nextSceneAsKOS))
            {
                if (cooldown == false)
                {
                    cooldown = true;
                    StartCoroutine("ShootingCooldown");
                    StartCoroutine("SpawnAxe");
                }
            }
            if (Input.GetKey(KeyCode.W) && Application.loadedLevelName.Equals(StateController.nextSceneAsKOS))
            {
                increaseAngel();
                increaseAimAngel();
            }
            if (Input.GetKey(KeyCode.S) && Application.loadedLevelName.Equals(StateController.nextSceneAsKOS))
            {        
                decreaseAngel();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "CannonBall01")
        {
            if (Application.loadedLevelName.Equals(StateController.nextSceneAsKOS))
            {
                StateController.ConsecutiveHitsValue = 0;
            } else if (Application.loadedLevelName.Equals(StateController.nextSceneAsMilo))
            {
                StateController.ConsecutiveHitsValue += 1;
            }
            Destroy(col.gameObject);
        }
    }

    public void increaseAimAngel()
    {
        curRot.z += Input.GetAxis("Horizontal") * Time.deltaTime * aimSpeed;
        curRot.z = Mathf.Clamp(curRot.z, minZ, maxZ);
        aimingArm.transform.eulerAngles = curRot;
    }

    /// <summary>
    /// Increases the angel.
    /// </summary>
    public void increaseAngel()
    {
        if (angel < MaxAngel)
        { 
            this.angel += 2; 
        }
    }

    /// <summary>
    /// Decreases the angel.
    /// </summary>
    public void decreaseAngel()
    {
        if (angel > MinAngel)
        {
            this.angel -= 2;
        }
    } 

    /// <summary>
    /// Spawns the axe.
    /// </summary>
    /// <returns>The axe.</returns>
    public IEnumerator SpawnAxe()
    {
        anim.SetTrigger(axeThrow);
        yield return new WaitForSeconds(0.6f);//wait until the animation is done playing, then throw the axe.
        axe = Instantiate(rotatingAxePrefab, new Vector2(kos.transform.position.x + vMeasures.x, kos.transform.position.y + vMeasures.y), Quaternion.identity) as GameObject;
        axe.name = "RotatingAxe01";
        axe.rigidbody2D.AddForce(Vector2.up * angel + Vector2.right * 500, ForceMode2D.Force);
    }
   
    /// <summary>
    /// Shootings the cooldown.
    /// </summary>
    /// <returns>The cooldown.</returns>
    public IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        cooldown = false;
    }
}
