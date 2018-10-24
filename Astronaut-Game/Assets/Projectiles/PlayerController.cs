using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 1.0f; // Default speed sensitivity
    public GameObject projectileTemplate;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject shot = Instantiate(projectileTemplate, this.transform.position, Quaternion.identity);
            Rigidbody rb = shot.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 10);

            GameObject shot2 = Instantiate(projectileTemplate, this.transform.position, Quaternion.identity);
            Rigidbody rb2 = shot2.GetComponent<Rigidbody>();
            rb2.velocity = new Vector3(0, 10, 0);

            GameObject shot3 = Instantiate(projectileTemplate, this.transform.position, Quaternion.identity);
            Rigidbody rb3 = shot3.GetComponent<Rigidbody>();
            rb3.velocity = new Vector3(10, 0, 0);
        }
    }
}
