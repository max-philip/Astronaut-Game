using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatVariables : MonoBehaviour {

    public static float maxHealth = 100f;
    public static float maxFuel = 200f;
    public static int headLamp = 1;
    public static float sprintMulti = 1.5f;
    public static int weapon = 1;

    public static int money = 500;

    //public static float value = 100;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        /*
        value += (Time.deltaTime * 1.5f);

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("LOGGING STATIC: " + value.ToString());
        }
        */
	}
}
