using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movableScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Floor")
        {
            Destroy(GetComponent<Rigidbody>());
        }
    }
}
