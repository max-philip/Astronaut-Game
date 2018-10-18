using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor;
//using UnityEditor.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayDesert()
    {
        SceneManager.LoadScene("Scenes/DesertLevel");
    }

    public void PlayGrass()
    {
        SceneManager.LoadScene("Scenes/GrassLevel");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Scenes/Tutorial");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
