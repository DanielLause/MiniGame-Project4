﻿//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using System.Collections;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public Light MyLight;
    public Transform Player;
    public Transform DroneNavAgent;
    public Shoot WeaponShootScript;
    public Animator MyAnimator;
    public float FlySpeed = 10;
    public float MaxDistance = 3;
    public float FlyUpSpeed = 2;
    public float Height = 2;
    public int LightEffect = 5;
    public bool CanFly = true;

    private Rigidbody myRigid;
    private NavMeshAgent myAgent;
    private int lightEffectTimer = 0;

    //private bool lightOn = false;
    private bool followPlayer = false;

    private bool haveATarget = false;
    private bool grounded = true;
    private bool playFlyAnimation = true;
    private bool landing;

    private void Awake()
    {
        //MyAnimator = transform.GetComponent<Animator>();
        myRigid = transform.GetComponent<Rigidbody>();
        myAgent = DroneNavAgent.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MyAnimator.SetFloat("Velocity", myAgent.velocity.magnitude);
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    CanFly = !(CanFly);
        //}
        myAgent.speed = FlySpeed;

        Vector3 endPosition = new Vector3(myAgent.pathEndPosition.x, myAgent.nextPosition.y, myAgent.pathEndPosition.z);

        if (Vector3.Distance(myAgent.nextPosition, endPosition) < 6)
        {
            if (!playFlyAnimation)
            {
                //MyAnimator.Play("StopFly");
            }
        }
        fly();
    }

    private void fly()
    {
        if (grounded)
        {
            transform.localPosition = Vector3.zero;
            myRigid.AddForce(Vector3.zero);
        }
        if (CanFly)
        {
            {
                if (Input.GetButtonDown("DroneFlyToTarget"))
                {
                    followPlayer = false;
                    haveATarget = true;
                    playFlyAnimation = true;
                }
                else if (Input.GetButtonDown("DroneFollow"))
                {
                    followPlayer = true;
                    playFlyAnimation = true;
                }
                else if (Input.GetButtonDown("DroneMoveUp") && !grounded)
                {
                    CanFly = false;
                    followPlayer = false;
                    //landing = true;
                }

                if (transform.position.y >= Height)
                {
                    transform.position = new Vector3(transform.position.x, Height, transform.position.z);
                    myRigid.velocity = Vector3.zero;
                    if (lightEffectTimer <= LightEffect)
                    {
                        StartCoroutine(lightEffect(0.2f));
                        lightEffectTimer++;
                    }

                    if (followPlayer)
                    {
                        myAgent.SetDestination(Player.position);
                        if (playFlyAnimation)
                        {
                            //MyAnimator.Play("StartFly");
                        }
                        playFlyAnimation = false;
                    }
                    else
                    {
                        if (WeaponShootScript.DroneTarget != null && haveATarget)
                        {
                            if (playFlyAnimation)
                            {
                                //MyAnimator.Play("StartFly");
                            }
                            playFlyAnimation = false;

                            myAgent.SetDestination(WeaponShootScript.DroneTarget);
                            haveATarget = false;
                        }
                    }
                }
                else if (Input.GetButtonDown("DroneFlyToTarget") || Input.GetButtonDown("DroneFollow") || (Input.GetButtonDown("DroneMoveUp") && grounded))
                {
                    myRigid.AddForce(0, (FlyUpSpeed * 1000) * Time.deltaTime, 0);
                    grounded = false;
                    //CanFly = true;
                    //landing = false;
                }
            }
        }
        if (!CanFly && !grounded)
        {
            if (transform.localPosition.y > 0)
            {
                myRigid.AddForce(0, -(FlyUpSpeed * 100) * Time.deltaTime, 0);
                myAgent.SetDestination(transform.position);
            }
            else
            {
                grounded = true;
                CanFly = true;
                myRigid.velocity = Vector3.zero;
                MyLight.gameObject.SetActive(false);
                lightEffectTimer = 0;
            }
        }
    }

    private void lightEffect()
    {
    }

    private IEnumerator lightEffect(float time)
    {
        MyLight.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        MyLight.gameObject.SetActive(true);
    }
}