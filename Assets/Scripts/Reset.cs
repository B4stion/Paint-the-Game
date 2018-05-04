using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {

    public string name;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(name);
        }
        if (collision.gameObject.tag == "Movable")
        {
            Vector3 pos = GameObject.Find("Player").transform.position;
            pos.y = pos.y + 3;
            collision.gameObject.transform.position = pos;
        }
    }
}
