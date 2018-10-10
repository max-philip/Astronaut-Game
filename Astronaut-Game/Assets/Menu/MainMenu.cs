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

    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/DesertLevel");

        //EditorSceneManager.OpenScene("Application.dataPath/Assets/Scenes/TestScene.unity", OpenSceneMode.Additive);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
