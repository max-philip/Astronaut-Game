﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {

    public float zoneRadius = 20f;
    public float speed = 8f;

    NavMeshAgent agent;
    //public Transform attackTarget;
    GameObject attackTarget;
    Transform targetTrans;


    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    Rigidbody rigidbody;

    Vector3 currentWander = new Vector3(0, -60, 0);
    float changeDirectionTime = 0.0f;

    private int health = 30;

    private GameObject gameController;

    public GameObject deadModel;

    private bool isChasing = false;



    // Use this for initialization
    void Start () {

        agent = this.GetComponent<NavMeshAgent>();
        rigidbody = this.GetComponent<Rigidbody>();

        attackTarget = GameObject.Find("StylizedAstronaut");
        targetTrans = attackTarget.transform;

        gameController = GameObject.Find("GameController");

    }

    // Update is called once per frame
    void Update () {

        Renderer rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("_Color");

        if (health >= 30)
        {
            rend.material.SetColor("_Color", Color.green);
        } else if (health >= 15)
        {
            rend.material.SetColor("_Color", Color.yellow);
        } else if (health > 0)
        {
            rend.material.SetColor("_Color", Color.red);
        } else
        {

            /*
            GameObject deadBrain = Instantiate(deadModel, this.transform.GetChild(0).position, this.transform.GetChild(0).rotation);
            Rigidbody rb = deadBrain.GetComponent<Rigidbody>();
            //rb.velocity = this.transform.GetComponent<Rigidbody>().velocity;

            rb.AddForce(transform.forward * 10);
            */

            Destroy(this.gameObject);
            

            gameController.GetComponent<GameController>().killEnemy();
            StatVariables.money += 20;
        }

        float dist = Vector3.Distance(transform.position, targetTrans.position);

        if (dist <= zoneRadius && dist >= 2.5f)
        {
            moveToTarget();
            if (!isChasing)
            {
                GameObject.Find("SoundController").GetComponent<PlanetSounds>().playEnemyChaseSound();
            }
            isChasing = true;

        }
        else
        {
            wander();
            
            isChasing = false;
        }

        if (dist < 3)
        {
            GameObject.Find("StylizedAstronaut").GetComponent<PlayerControl>().reduceHealth(1);
            Debug.Log("damage");
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }

    private void moveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTrans.position, Time.deltaTime*speed);
        transform.LookAt(targetTrans.position);
    }

    private void wander()
    {
        
        Vector3[] AIwanders = new Vector3[] { new Vector3(0, 60, 0), new Vector3(0, -60, 0) ,
                                        new Vector3(60, 0, 0), new Vector3(-60, 0, 0),
                                        new Vector3(0, 0, 60), new Vector3(0, 0, -60)};

        
        /*
        Vector3[] AIwanders = new Vector3[] { transform.forward*3, transform.right*3,
                                        -transform.forward*3, -transform.right*3 };
        */

        float wanderSpeed;
        float wanderDist = Vector3.Distance(transform.position, currentWander);

        if (Time.time > changeDirectionTime)
        {
            currentWander = AIwanders[Random.Range(0, AIwanders.Length)];
            changeDirectionTime += Random.Range(1f, 2.5f);
        }
        
        if (wanderDist > 60)
        {
            wanderSpeed = speed / 2;
        } else
        {
            wanderSpeed = speed / 4;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentWander, Time.deltaTime * wanderSpeed);
        transform.LookAt(currentWander);

    }

    public void getDamaged(int damage)
    {
        health -= damage;
    }
}
