using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour {

    public Rect windowRect;
    public GUIStyle windowStyle;
    private bool firstSeconds;
    public string levelName;

	// Use this for initialization
	void Start () {

        windowStyle.alignment = TextAnchor.MiddleCenter;
        windowStyle.fontSize = 25;

		windowRect = new Rect(Screen.width / 2 - 200, Screen.height/2 - 250, 400, 600);

        levelName = "Tutorial";
        firstSeconds = true;
        Invoke("DisableLevelName", 4);

        DetermineLevelName();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

    void OnGUI()
    {
        if (Time.timeScale == 0)
        {
            windowRect = GUI.Window(0, windowRect, MakeWindow, "Menu");
        }
        //Display level name for first few seconds
        if (firstSeconds == true)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 50;
            style.alignment = TextAnchor.UpperCenter;
            GUI.Label(new Rect(Screen.width/2-150, 50, 300, 40), levelName, style);
        }
    }

    void MakeWindow(int windowID)
    {
        //Add a button to restart the current level, replacing the previous functionality of ESC
        if (GUI.Button(new Rect(windowRect.width/2 - 80, 50, 160, 30), "Reset"))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        //Add buttons for the current  levels
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 100, 160, 30), "Level 1: Bounce"))
        {
            SceneManager.LoadScene("levelBounce", LoadSceneMode.Single);
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 150, 160, 30), "Level 2: Slide"))
        {
            SceneManager.LoadScene("levelSlide");
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 200, 160, 30), "Level 3: Float"))
        {
            SceneManager.LoadScene("levelFloat");
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 250, 160, 30), "Level 4: Hit and Run"))
        {
            SceneManager.LoadScene("levelFastSlide");
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 300, 160, 30), "Level 5: Underneath"))
        {
            SceneManager.LoadScene("levelUnderneath");
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 350, 160, 30), "Level 6: Acid Clouds"))
        {
            SceneManager.LoadScene("levelNoPaintZone");
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 400, 160, 30), "Level 7: Spotlights"))
        {
            SceneManager.LoadScene("levelDeathRays");
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 450, 160, 30), "Level 8: Skipping Stones"))
        {
            SceneManager.LoadScene("levelSkippingStone");
            Time.timeScale = 1;
        }
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 500, 160, 30), "Level 9: Waterfalls"))
        {
            SceneManager.LoadScene("levelWaterfall");
            Time.timeScale = 1;
        }
        //Add a button to quit the game
        if (GUI.Button(new Rect(windowRect.width / 2 - 80, 550, 160, 30), "Quit Game"))
        {
            Application.Quit();
            Time.timeScale = 1;
        }
    }

    void DisableLevelName()
    {
        firstSeconds = false;
    }

    void DetermineLevelName()
    {
        if (SceneManager.GetActiveScene().name == "levelBounce")
        {
            levelName = "Level 1: Bounce";
        }
        else if (SceneManager.GetActiveScene().name == "levelSlide")
        {
            levelName = "Level 2: Slide";
        }
        else if (SceneManager.GetActiveScene().name == "levelFloat")
        {
            levelName = "Level 3: Float";
        }
        else if (SceneManager.GetActiveScene().name == "levelFastSlide")
        {
            levelName = "Level 4: Hit and Run";
        }
        else if (SceneManager.GetActiveScene().name == "levelUnderneath")
        {
            levelName = "Level 5: Underneath";
        }
        else if (SceneManager.GetActiveScene().name == "levelNoPaintZone")
        {
            levelName = "Level 6: Acid Clouds";
        }
        else if (SceneManager.GetActiveScene().name == "levelDeathRays")
        {
            levelName = "Level 7: Spotlights";
        }
        else if (SceneManager.GetActiveScene().name == "levelSkippingStone")
        {
            levelName = "Level 8: Skipping Stones";
        }
        else if (SceneManager.GetActiveScene().name == "levelWaterfall")
        {
            levelName = "Level 9: Waterfalls";
        }
    }
}
