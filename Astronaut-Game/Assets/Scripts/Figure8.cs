using UnityEngine;
using System.Collections;

public class Figure8 : MonoBehaviour {

    public float radius;
    public float rotationSpeed;

    private int phase = 1;
    private float currentAngle = 0.0f;
	
	// Update is called once per frame
	void Update () {
        // Reset local transform each frame since we are tracking an 'absolute' angle
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        // Use Unity's built-in RotateAround method to perform an orbit -
        // Note that the pivot point and axis for the orbit is in world coordinates
        // so we need to make conversions from local to world coordinates first
        // in order to properly preseve any parent transformations. (As an experiment
        // try removing the conversions and play around with the parent entity's
        // transform.)
        Vector3 pivotPointInWorldSpace = this.transform.TransformPoint(Vector3.forward * this.phase * this.radius);
        Vector3 axisInWorldSpace = this.transform.TransformDirection(Vector3.up);
        this.transform.RotateAround(pivotPointInWorldSpace, axisInWorldSpace, this.currentAngle * phase);

        // Switch the phase on each full rotation about the axis to achieve the 
        // figure 8 effect
        if (Mathf.FloorToInt(currentAngle / 360.0f) % 2 == 0)
            this.phase = 1;
        else
            this.phase = -1;

        // Cumulatively keep track of an 'absolute' rotation angle
        this.currentAngle += Time.deltaTime * this.rotationSpeed;
	}
}
