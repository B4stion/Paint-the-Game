// Authors: Griffin Shaw
// Details: Script that dictates behavior of exit door 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour {

	GameObject player;
    public string nextScene;
	private string currentScene;
	private int nextSceneIndex;
    

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		currentScene = SceneManager.GetActiveScene().name;
		//nextSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}

    // Update is called once per frame
	void Update() {
	}

    // On collission with player opbject, load into a new scene
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.name == "Player")
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
