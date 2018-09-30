using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevitateMana : MonoBehaviour {

    public Image LevitateManaBar;

	// Use this for initialization
	void Start () {
        PlayerControl.LevitateManaChange += onLevitateManaChange;
	}

    private void onLevitateManaChange(float levitateMana)
    {
        RectTransform levRectTransform = LevitateManaBar.GetComponent<RectTransform>();
        levRectTransform.sizeDelta = new Vector2(levitateMana*4, 10.0f);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
