using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image HBar;

    // Use this for initialization
    void Start()
    {
        global.HealthChange += onHealthChange;
    }

    private void onHealthChange(float health)
    {
        RectTransform healthRectTransform = HBar.GetComponent<RectTransform>();
        healthRectTransform.sizeDelta = new Vector2(health * 4, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
