using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevitateMana : MonoBehaviour
{

    public Image JetBar;

    // Use this for initialization
    void Start()
    {
        global.JetChange += onJetUsed;
    }

    private void onJetUsed(float fuel)
    {
        RectTransform jetRectTransform = JetBar.GetComponent<RectTransform>();
        jetRectTransform.sizeDelta = new Vector2(fuel * 4, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
