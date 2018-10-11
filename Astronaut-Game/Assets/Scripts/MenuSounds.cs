using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour {

    AudioSource audio;
    public AudioClip buttonClick;

    // Use this for initialization
    void Start () {
        audio = gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void buttonSound()
    {
        audio.PlayOneShot(buttonClick);
    }
}
