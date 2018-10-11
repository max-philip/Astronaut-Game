using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class projectileController : MonoBehaviour {

    public GameObject projectileExplosion;

    private GameObject gameController;

    //AudioSource audio;
    //public AudioClip hitSound;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController");

        //audio = gameObject.AddComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Destroy(this.gameObject, 30.0f);

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        GameObject explode = Instantiate(projectileExplosion);
        explode.transform.position = this.transform.position;


        if (StatVariables.weapon == 1)
        {
            GameObject.Find("SoundController").GetComponent<PlanetSounds>().playRock();
        }
        else if (StatVariables.weapon == 2)
        {
            GameObject.Find("SoundController").GetComponent<PlanetSounds>().playGrenade();
        }
        else
        {
            GameObject.Find("SoundController").GetComponent<PlanetSounds>().playPulse();
        }

        if (other.gameObject.tag == "Destructible"){
            if (SceneManager.GetActiveScene().name != "Tutorial")
            {
                if (StatVariables.weapon == 1)
                {
                    other.gameObject.GetComponent<EnemyControl>().getDamaged(10);
                }
                else if (StatVariables.weapon == 2)
                {
                    other.gameObject.GetComponent<EnemyControl>().getDamaged(20);
                }
                else
                {
                    other.gameObject.GetComponent<EnemyControl>().getDamaged(40);
                }
            }
            else
            {
                // TUTORIAL ONLY

                StatVariables.money += 5;
                Destroy(other.gameObject);
                gameController.GetComponent<GameController>().killEnemy();
            }


       
            /*
            if (gameController.GetComponent<GameController>().getEnemyCount() == 0)
            {
                PlayerControl myControl = GameObject.Find("Stylized Astronaut").GetComponent<PlayerControl>();
                myControl.cubesCollected = true;
            }
            */

        }
    }
}
