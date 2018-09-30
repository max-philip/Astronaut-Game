using UnityEngine;
using System.Collections;

public class MoveForwardAndBackward : MonoBehaviour {

    public float thresholdDistanceUpper;
    public float thresholdDistanceLower;

    public float speed;

    private int dir = 1; // Switches when cube reverses direction

    // Update is called once per frame
    void Update () {
        this.transform.localPosition += Vector3.up * Time.deltaTime * speed * dir;
        if (this.transform.localPosition.y > thresholdDistanceUpper)
            dir = -1;
        else if (this.transform.localPosition.y < thresholdDistanceLower)
            dir = 1;
    }
}
