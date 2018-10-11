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
        string mins = Mathf.Floor(timer / 60).ToString("00");
        string sec = (timer % 60).ToString("00");

        if (enemies > 0)
        {
            timer += Time.deltaTime;
            timerText.text = "Time Elapsed:   " + string.Format("{0}:{1}", mins, sec);
        }
        else
        {
            timerText.text = "Time Elapsed:   " + string.Format("{0}:{1}", mins, sec);
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
        countText.text = "Enemies Remaining: " + getEnemyCount();
    }
}
