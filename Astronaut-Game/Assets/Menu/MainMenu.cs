using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor;
//using UnityEditor.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Scenes/DesertScene");

        //EditorSceneManager.OpenScene("Application.dataPath/Assets/Scenes/TestScene.unity", OpenSceneMode.Additive);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
