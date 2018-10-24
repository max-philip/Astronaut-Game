using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bestTimes : MonoBehaviour {

    private Text TutorialTime;
    private Text DesertTime;
    private Text GrassTime;

	// Use this for initialization
	void Start () {
        TutorialTime = GameObject.Find("TutorialTime").GetComponent<Text>();
        DesertTime = GameObject.Find("DesertTime").GetComponent<Text>();
        GrassTime = GameObject.Find("GrassTime").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        setTutorialTime();
        setDesertTime();
        setGrassTime();
    }

    private void setTutorialTime()
    {
        if (StatVariables.tutorialMins.Equals(""))
        {
            TutorialTime.text = "BEST TIME: --:--";
        } else
        {
            TutorialTime.text = "BEST TIME: " + StatVariables.tutorialMins + ":" + StatVariables.tutorialSec;
        }
    }

    private void setDesertTime()
    {
        if (StatVariables.desertMins.Equals(""))
        {
            DesertTime.text = "BEST TIME: --:--";
        }
        else
        {
            DesertTime.text = "BEST TIME: " + StatVariables.desertMins + ":" + StatVariables.desertSec;
        }
    }

    private void setGrassTime()
    {
        if (StatVariables.grassMins.Equals(""))
        {
            GrassTime.text = "BEST TIME: --:--";
        }
        else
        {
            GrassTime.text = "BEST TIME: " + StatVariables.grassMins + ":" + StatVariables.grassSec;
        }
    }
}
