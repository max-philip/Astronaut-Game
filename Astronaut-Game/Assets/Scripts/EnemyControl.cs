using System.Collections;
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

    


    // Use this for initialization
    void Start () {

        agent = this.GetComponent<NavMeshAgent>();
        rigidbody = this.GetComponent<Rigidbody>();

        attackTarget = GameObject.Find("StylizedAstronaut");
        targetTrans = attackTarget.transform;
    }
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(transform.position, targetTrans.position);

        if (dist <= zoneRadius && dist >= 3)
        {
            //agent.SetDestination(attackTarget.position);

            moveToTarget();
        } else
        {
            wander();
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
}
