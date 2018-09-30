using UnityEngine;
using System.Collections;

public class UpDown : MonoBehaviour {

    public float speed = 1;

    public int distMax = 10;
    
    private int dir = 1; // Switches when cube reverses direction

    private Vector3 startPos;
    private float dist;

    private void Start()
    {
        startPos = transform.position;
        dist = 0.0f;
    }

    // Update is called once per frame
    void Update () {

        dist = Vector3.Distance(startPos, transform.position);
        if (dist < distMax && dir == 1)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else if (dist > distMax && dir == 1)
        {
            transform.position += transform.forward * Time.deltaTime * speed * -1;
            dir = -1;
        }
        else if (dist > 1 && dir == -1)
        {
            transform.position += transform.forward * Time.deltaTime * speed * -1;
        } else if (dist < 1 && dir == -1)
        {
            transform.position += transform.forward * Time.deltaTime * speed * -1;
            dir = 1;
        }
    }
}
