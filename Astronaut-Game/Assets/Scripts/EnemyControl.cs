using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyControl : MonoBehaviour {

    public float zoneRadius = 20f;
    public float speed = 8f;

    GameObject attackTarget;
    Transform targetTrans;


    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    Rigidbody rigidbody;

    Vector3 currentWander = new Vector3(0, -60, 0);
    float changeDirectionTime = 0.0f;

    private int health = 50;

    private GameObject gameController;

    public GameObject deadModel;

    private bool isChasing = false;


    // Use this for initialization
    void Start () {

        rigidbody = this.GetComponent<Rigidbody>();

        attackTarget = GameObject.Find("StylizedAstronaut");
        targetTrans = attackTarget.transform;

        gameController = GameObject.Find("GameController");


    }

    // Update is called once per frame
    void Update () {

        
        if (gameController.GetComponent<GameController>().getEnemyCount() <= 6)
        {
            this.zoneRadius = 125.0f;
        } else
        {
            this.zoneRadius = 35.0f;
        }


        Renderer rend = GetComponent<Renderer>();

        if (health >= 50)
        {
            rend.material.SetColor("_Color", Color.green);
        }
        else if (health >= 40)
        {
            rend.material.SetColor("_Color", Color.yellow);
        }
        else if (health >= 30)
        {
            rend.material.SetColor("_Color", Color.yellow);
        }
        else if (health >= 20)
        {
            rend.material.SetColor("_Color", Color.yellow);
        }
        else if (health > 0)
        {
            rend.material.SetColor("_Color", Color.red);
        }
        else
        {
            

            Destroy(this.gameObject);
            
            gameController.GetComponent<GameController>().killEnemy();
            StatVariables.money += 20;
        }

        float dist = Vector3.Distance(transform.position, targetTrans.position);

        if (dist <= zoneRadius && dist > 3.15f)
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

        if (dist < 3.15f)
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
