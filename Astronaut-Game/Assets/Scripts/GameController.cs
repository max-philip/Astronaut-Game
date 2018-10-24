using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
public class GameController : MonoBehaviour
{

    public int enemies;

    private Text countText;

    private float timer = 0.0f;
    private Text timerText;

    private string mins;
    private string sec;

    public GameObject victoryUI;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        countText = GameObject.Find("CountText").GetComponent<Text>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //timer += Time.deltaTime;
        mins = Mathf.Floor(timer / 60).ToString("00");
        sec = (timer % 60).ToString("00");

        if (enemies > 0)
        {
            timer += Time.deltaTime;
            timerText.text = "TIME ELAPSED:   " + string.Format("{0}:{1}", mins, sec);
        }
        else
        {
            timerText.text = "TIME ELAPSED:   " + string.Format("{0}:{1}", mins, sec);
            victoryUI.SetActive(true);
            newScore(mins, sec);
        }


        setCountText();
    }

    public int getEnemyCount()
    {
        return enemies;
    }

    public void killEnemy()
    {
        enemies -= 1;
    }

    private void setCountText()
    {
        countText.text = "ENEMIES REMAINING: " + getEnemyCount();
    }

    public string getCurrTime()
    {
        return string.Format("{0}:{1}", mins, sec);
    }

    private void newScore(string newMin, string newSec)
    {
        int myNewMin = Convert.ToInt32(newMin);
        int myNewSec = Convert.ToInt32(newSec);


        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (StatVariables.tutorialMins == "" && StatVariables.tutorialSec == "")
            {
                StatVariables.tutorialMins = newMin;
                StatVariables.tutorialSec = newSec;
            } else
            {
                if (((myNewMin < Convert.ToInt32(StatVariables.tutorialMins)) ||
                (myNewMin == Convert.ToInt32(StatVariables.tutorialMins) && myNewSec < Convert.ToInt32(StatVariables.tutorialSec))))
                {
                    StatVariables.tutorialMins = newMin;
                    StatVariables.tutorialSec = newSec;
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "DesertLevel")
        {
            if (StatVariables.desertMins == "" && StatVariables.desertSec == "")
            {
                StatVariables.desertMins = newMin;
                StatVariables.desertSec = newSec;
            }
            else
            {
                if (((myNewMin < Convert.ToInt32(StatVariables.desertMins)) ||
                (myNewMin == Convert.ToInt32(StatVariables.desertMins) && myNewSec < Convert.ToInt32(StatVariables.desertSec))))
                {
                    StatVariables.desertMins = newMin;
                    StatVariables.desertSec = newSec;
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "TerraLevel")
        {
            if (StatVariables.grassMins == "" && StatVariables.grassSec == "")
            {
                StatVariables.grassMins = newMin;
                StatVariables.grassSec = newSec;
            }
            else
            {
                if (((myNewMin < Convert.ToInt32(StatVariables.grassSec)) ||
                (myNewMin == Convert.ToInt32(StatVariables.grassMins) && myNewSec < Convert.ToInt32(StatVariables.grassSec))))
                {
                    StatVariables.grassMins = newMin;
                    StatVariables.grassSec = newSec;
                }
            }
        }
    }

}
