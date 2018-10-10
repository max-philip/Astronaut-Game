using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Equipment : MonoBehaviour {

    private Text MoneyText;


    private void Start()
    {
        MoneyText = GameObject.Find("MoneyText").GetComponent<Text>();
    }
    private void Update()
    {
        MoneyText.text = "SPACE BUCKS:  " + StatVariables.money.ToString();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/DesertLevel");

        //EditorSceneManager.OpenScene("Application.dataPath/Assets/Scenes/TestScene.unity", OpenSceneMode.Additive);
    }

    private bool checkMoney(int cost)
    {
        if (StatVariables.money >= cost)
        {
            StatVariables.money -= cost;
            return true;
        } else
        {
            return false;
        }
    }


    public void sprint1()
    {
        StatVariables.sprintMulti = 1.5f;
    }

    public void sprint2()
    {
        if (checkMoney(110))
        {
            StatVariables.sprintMulti = 2.0f;
        }
    }

    public void sprint3()
    {
        if (checkMoney(220))
        {
            StatVariables.sprintMulti = 2.5f;
        }
    }






    public void headLamp1()
    {
        StatVariables.headLamp = 1;
    }

    public void headLamp2()
    {
        if (checkMoney(160))
        {
            StatVariables.headLamp = 2;
        }
    }






    public void weaponRock()
    {
        StatVariables.weapon = 1;
    }

    public void weaponGrenade()
    {
        if (checkMoney(50))
        {
            StatVariables.weapon = 2;
        }
    }

    public void weaponPulse()
    {
        if (checkMoney(150))
        {
            StatVariables.weapon = 3;
        }
    }






    public void fuel200()
    {
        StatVariables.maxFuel = 200;
    }

    public void fuel300()
    {
        if (checkMoney(100))
        {
            StatVariables.maxFuel = 300;
        }
    }

    public void fuel400()
    {
        if (checkMoney(220))
        {
            StatVariables.maxFuel = 400;
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
        if (checkMoney(250))
        {
            StatVariables.maxHealth = 250;
        }
    }

}
