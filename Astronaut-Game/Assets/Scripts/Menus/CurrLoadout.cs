using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrLoadout : MonoBehaviour {


    private Text weaponText;
    private Text healthText;
    private Text sprintText;
    private Text fuelText;


    // Use this for initialization
    void Start () {
        weaponText = GameObject.Find("weapon").GetComponent<Text>();
        healthText = GameObject.Find("health").GetComponent<Text>();
        sprintText = GameObject.Find("sprint").GetComponent<Text>();
        fuelText = GameObject.Find("fuel").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        weaponText.text = getWeapon(StatVariables.weapon);
        healthText.text = StatVariables.maxHealth.ToString() + "pts";
        sprintText.text = "x " + StatVariables.sprintMulti.ToString();
        fuelText.text = StatVariables.maxFuel.ToString() + "pts";
    }




    string getWeapon(int weaponID)
    {
        if (weaponID == 1)
        {
            return "ROCK";
        }

        if (weaponID == 2)
        {
            return "GRENADE";
        }

        if (weaponID == 3)
        {
            return "PULSE BOMB";
        }

        return "N/A";
    }
}
