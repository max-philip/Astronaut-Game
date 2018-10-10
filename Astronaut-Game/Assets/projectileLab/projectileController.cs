using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projectileController : MonoBehaviour {

    public GameObject projectileExplosion;

    private BoxScoring boxes;
    private Text countText;


    // Use this for initialization
    void Start () {
        boxes = GameObject.Find("CountText").GetComponent<BoxScoring>();
        countText = GameObject.Find("CountText").GetComponent<Text>();
        setCountText();
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
            boxes.boxCount -= 1;
            Debug.Log(boxes.boxCount);
            setCountText();

            StatVariables.money += 10;

            if (boxes.boxCount == 0)
            {
                PlayerControl myControl = GameObject.Find("Stylized Astronaut").GetComponent<PlayerControl>();
                myControl.cubesCollected = true;
            }

        }
    }

    private void setCountText()
    {
        countText.text = "Enemies Remaining: " + boxes.boxCount.ToString();
    }
}
