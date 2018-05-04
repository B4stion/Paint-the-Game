using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScript : MonoBehaviour {

    public bool endTitle;
    private GUIStyle style;
    private Rect window;
    public Texture background;

	// Use this for initialization
	void Start () {

        endTitle = false;
        window = new Rect(0, 0,Screen.width, Screen.height);
        

        Invoke("EndTheTitle", 3);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (endTitle == true)
        {
            SceneManager.LoadScene("levelTutorial", LoadSceneMode.Single);
        }
	}

    void OnGUI()
    {
        window = GUI.Window(0, window, MakeWindow, "");

        GUIStyle styleR = new GUIStyle();
        styleR.normal.textColor = Color.red;
        styleR.fontSize = 300;
        styleR.alignment = TextAnchor.UpperCenter;

        GUIStyle styleB = new GUIStyle();
        styleB.normal.textColor = Color.blue;
        styleB.fontSize = 300;
        styleB.alignment = TextAnchor.UpperCenter;

        GUIStyle styleY = new GUIStyle();
        styleY.normal.textColor = Color.yellow;
        styleY.fontSize = 300;
        styleY.alignment = TextAnchor.UpperCenter;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.fontSize = 30;
        style.alignment = TextAnchor.UpperCenter;

        GUI.Label(new Rect(Screen.width / 2 - 430, 200, 30, 40), "P", styleR);
        GUI.Label(new Rect(Screen.width / 2 - 230, 200, 30, 40), "A", styleB);
        GUI.Label(new Rect(Screen.width / 2 - 30, 200, 30, 40), "I", styleY);
        GUI.Label(new Rect(Screen.width / 2 + 170, 200, 30, 40), "N", styleR);
        GUI.Label(new Rect(Screen.width / 2 + 370, 200, 30, 40), "T", styleB);

        GUI.Label(new Rect(Screen.width / 2 - 50, 600, 100, 50), "Created by\nScott Hodnefield Griffin Shaw\nRyun Han and Zach Zhang", style);

    }

    void MakeWindow(int windowID)
    {

    }

    void EndTheTitle()
    {
        endTitle = true;
    }
}
