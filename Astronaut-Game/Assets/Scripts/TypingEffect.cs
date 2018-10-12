using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour {

    public string finalText;
    private string currText = "";

    public float spacing = 0.005f;

	// Use this for initialization
	void Start () {
        StartCoroutine(WriteText());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator WriteText()
    {
        for (int i=0; i<=finalText.Length; i++)
        {
            currText = finalText.Substring(0, i);
            GetComponent<Text>().text = currText;

            yield return new WaitForSeconds(spacing);
        }
    }
}
