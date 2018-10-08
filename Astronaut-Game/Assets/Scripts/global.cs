using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class global : MonoBehaviour {

    public static float health = 100f;
    public static float fuel = 100f;

    public static event Action<float> HealthChange;
    public static event Action<float> JetChange;

    // Use this for initialization
    void Start () {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(health);

        HealthChange(health);
        JetChange(fuel);


        if (health <= 0)
        {
            SceneManager.LoadSceneAsync("Scenes/DeathScene");
        }

        if (PauseMenu.gamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
