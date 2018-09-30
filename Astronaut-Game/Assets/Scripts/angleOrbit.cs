using UnityEngine;
using System.Collections;

public class angleOrbit : MonoBehaviour
{

    public float spinSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * spinSpeed, new Vector3(1.0f, 1.0f, 0.0f));
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.localPosition += Vector3.forward * Time.deltaTime;
        }
    }
}
