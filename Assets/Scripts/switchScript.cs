using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScript : MonoBehaviour {

    public string targetName;
    public string nextScene;
    public bool activated;
    public GameObject switchObject;
    public Material doorMat;

    // Use this for initialization
    void Start () {
        switchObject = GameObject.Find(targetName);
        GetComponent<MeshRenderer>().material.color = Color.black;
        switchObject.GetComponent<Renderer>().material.color = Color.black;
        activated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player" || collision.collider.gameObject.tag == "Movable" && !activated)
        {
            GameObject.Find(targetName).AddComponent<exit>();
            GameObject.Find(targetName).GetComponent<exit>().nextScene = nextScene;
            GetComponent<MeshRenderer>().material.color = Color.magenta;
            switchObject.GetComponent<Renderer>().material = doorMat;
            activated = true;
        }
    }
}
