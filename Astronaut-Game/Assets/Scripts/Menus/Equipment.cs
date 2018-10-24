using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Equipment : MonoBehaviour {

    private Text MoneyText;

    AudioSource audio;
    public AudioClip denied;
    public AudioClip success;


    private void Start()
    {
        MoneyText = GameObject.Find("MoneyText").GetComponent<Text>();

        audio = gameObject.AddComponent<AudioSource>();

    }
    private void Update()
    {
        MoneyText.text = "MONEY: $" + StatVariables.money.ToString();
    }

    private bool checkMoney(int cost)
    {
        if (StatVariables.money >= cost)
        {
            StatVariables.money -= cost;
            audio.PlayOneShot(success);
            return true;
        } else
        {
            audio.PlayOneShot(denied);
            return false;
        }
    }


    public void sprint1()
    {
        StatVariables.sprintMulti = 1.5f;
    }

    public void sprint2()
    {
        if (checkMoney(100))
        {
            StatVariables.sprintMulti = 2.0f;
        }
    }

    public void sprint3()
    {
        if (checkMoney(200))
        {
            StatVariables.sprintMulti = 2.5f;
        }
    }




    public void weaponRock()
    {
        StatVariables.weapon = 1;
    }

    public void weaponGrenade()
    {
        if (checkMoney(125))
        {
            StatVariables.weapon = 2;
        }
    }

    public void weaponPulse()
    {
        if (checkMoney(250))
        {
            StatVariables.weapon = 3;
        }
    }






    public void fuel1()
    {
        StatVariables.maxFuel = 50;
    }

    public void fuel2()
    {
        if (checkMoney(125))
        {
            StatVariables.maxFuel = 75;
        }
    }

    public void fuel3()
    {
        if (checkMoney(250))
        {
            StatVariables.maxFuel = 100;
        }
    }






    public void health100()
    {
        StatVariables.maxHealth = 100;
    }

    public void health150()
    {
        if (checkMoney(100))
        {
            StatVariables.maxHealth = 150;
        }
    }

    public void health250()
    {
        if (checkMoney(200))
        {
            StatVariables.maxHealth = 250;
        }
    }

}
