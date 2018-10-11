using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class projectileController : MonoBehaviour {

    public GameObject projectileExplosion;

    private GameObject gameController;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController");
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

        if (other.gameObject.tag == "Destructible")
        {

            other.gameObject.GetComponent<EnemyControl>().getDamaged(10);

            // Increase score for tutorial targets here - for BRAIN enemies, this is done in EnemyControl.cs
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                StatVariables.money += 5;
            }

            if (gameController.GetComponent<GameController>().getEnemyCount() == 0)
            {
                PlayerControl myControl = GameObject.Find("Stylized Astronaut").GetComponent<PlayerControl>();
                myControl.cubesCollected = true;
            }

        }
    }
}
