using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour {

    public GameObject projectileExplosion;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Destroy(this.gameObject, 10.0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        GameObject explode = Instantiate(projectileExplosion);
        explode.transform.position = this.transform.position;

        if (other.gameObject.tag == "Destructible")
        {
            Destroy(other.gameObject);
        }
    }
}
