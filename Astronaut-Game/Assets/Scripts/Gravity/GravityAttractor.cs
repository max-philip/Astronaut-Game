using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour
{

    public float gravity = -9.8f;


    public void Attract(Rigidbody rb)
    {
        Vector3 gravUp = (rb.position - transform.position).normalized;
        Vector3 up = rb.transform.up;
        rb.AddForce(gravUp * gravity);
        rb.rotation = Quaternion.FromToRotation(up, gravUp) * rb.rotation;
    }
}