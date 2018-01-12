using UnityEngine;
using System.Collections;

public class UIScoreScript : MonoBehaviour {

    public int fontSize;

	// Use this for initialization
	void Start () {

        SetFont();

    }

    // Update is called once per frame
    void Update () {


    }

    void UpdateScore()
    {
        
    }

    void SetFont()
    {
        GetComponent<GUIText>().fontSize = Mathf.Min(Screen.width, Screen.height) / fontSize;
    }
}
