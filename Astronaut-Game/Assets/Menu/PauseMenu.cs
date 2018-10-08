using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;

    public static bool gamePaused = false;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
	}

    public void QuitToDesktop()
    {
        Debug.Log("Quitting the game");

        Resume();
        Application.Quit();
    }

    public void AbandonMission()
    {
        Debug.Log("Going back to main menu");

        Resume();
        SceneManager.LoadScene("MainMenu");

    }

    void Pause()
    {
        gamePaused = true;
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        gamePaused = false;
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
