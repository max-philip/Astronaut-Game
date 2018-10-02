using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyControl : MonoBehaviour {

    public float zoneRadius = 25f;
    public float speed = 8f;

    NavMeshAgent agent;
    public Transform attackTarget;


    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    Rigidbody rigidbody;



    // Use this for initialization
    void Start () {

        agent = this.GetComponent<NavMeshAgent>();
        rigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(transform.position, attackTarget.position);

        if (dist <= zoneRadius && dist >= 3)
        {
            //agent.SetDestination(attackTarget.position);

            moveToTarget();
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }

    private void moveToTarget()
    {
        /*
        transform.LookAt(attackTarget);
        Vector3 moveDir = transform.forward;
        //Vector3 moveDir = new Vector3(moveX, 0, moveY).normalized;
        Vector3 targetMoveAmount = moveDir * speed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        //rigidbody.MovePosition(rigidbody.position + localMove);
        */

        transform.position = Vector3.MoveTowards(transform.position, attackTarget.position, Time.deltaTime*speed);
    }
}
