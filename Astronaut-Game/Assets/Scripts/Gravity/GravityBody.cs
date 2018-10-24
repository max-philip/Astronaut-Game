using UnityEngine;
using System.Collections;

public class GravityBody : MonoBehaviour
{

    GravityAttractor bigBody;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bigBody = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;

    }

    void FixedUpdate()
    {
        bigBody.Attract(rb);
    }
}