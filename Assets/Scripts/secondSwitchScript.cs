using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class secondSwitchScript : MonoBehaviour {

    public string targetLevel;
    public string targetName;
    private bool switchPressed;
    private GameObject switchTarget;
    private float timeLimit;
    private string currentScene;
    public Material doorMat;

	// Use this for initialization
	void Start () {
        switchPressed = false;
        switchTarget = GameObject.Find(targetName);
        switchTarget.GetComponent<Renderer>().material.color = Color.black;
        GetComponent<Renderer>().material.color = Color.black;
    }
	
	// Update is called once per frame
	void Update () {
		if (switchPressed == true)
        {
            if (Time.timeSinceLevelLoad - timeLimit > 10)
            {
                ResetExit();
            }
        }
	}
    //Trigger switch if player steps on it
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.tag == "Movable")
        {
            //Record time switch was pressed
            timeLimit = Time.timeSinceLevelLoad;

            switchPressed = true;
            //Change color of switch and exit
            switchTarget.GetComponent<Renderer>().material = doorMat;
            GetComponent<Renderer>().material.color = Color.magenta;
            //Attach exit script to exit and assign the next level
            switchTarget.AddComponent<exit>();
            switchTarget.GetComponent<exit>().nextScene = targetLevel;
        }
    }

    private void ResetExit()
    {
        //GameObject switchTarget = GameObject.Find("Object_16");
        //Reset Colors
        switchTarget.GetComponent<Renderer>().material.color = Color.black;
        GetComponent<Renderer>().material.color = Color.black;
        //Remove exit script
        Destroy(switchTarget.GetComponent<exit>());
        switchPressed = false;
    }
}
