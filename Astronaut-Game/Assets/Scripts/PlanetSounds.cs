using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSounds : MonoBehaviour {


    AudioSource audio;
    public AudioClip rockHit;
    public AudioClip grenadeHit;
    public AudioClip pulseHit;

    public AudioClip pain;

    //private bool isChasing = false;
    //private AudioSource chase;

    public AudioClip chase;

    // Use this for initialization
    void Start () {
        audio = gameObject.AddComponent<AudioSource>();

        //chase = GameObject.Find("enemychase").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void playRock()
    {
        audio.PlayOneShot(rockHit);
    }

    public void playGrenade()
    {
        audio.PlayOneShot(grenadeHit);
    }

    public void playPulse()
    {
        audio.PlayOneShot(pulseHit);
    }

    public void playPain()
    {
        audio.PlayOneShot(pain);
    }


    public void playEnemyChaseSound()
    {
        audio.PlayOneShot(chase);
    }



}
