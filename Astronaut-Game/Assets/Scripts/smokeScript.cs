using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeScript : MonoBehaviour {

    private GameObject player;

    AudioSource audio;
    public AudioClip jetSteam;

    // Use this for initialization
    void Start () {
		player = GameObject.Find("StylizedAstronaut");

        audio = gameObject.AddComponent<AudioSource>();
        audio.pitch = 0.1f;
        audio.volume = 0.05f;

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.Space) && player.GetComponent<PlayerControl>().getIsJetting())
        {
            if (!this.GetComponent<ParticleSystem>().isPlaying)
            {
                this.GetComponent<ParticleSystem>().Play();
                //this.GetComponent<ParticleSystem>().enableEmission = true;
            }

            if (!audio.isPlaying)
            {
                audio.PlayOneShot(jetSteam);
            }

        }
        else
        {

            if(audio.isPlaying)
            {
                audio.Stop();
            }


            if (this.GetComponent<ParticleSystem>().isPlaying)
            {
                this.GetComponent<ParticleSystem>().Stop();
                //this.GetComponent<ParticleSystem>().enableEmission = false;
            }
            
        }


    }
}
