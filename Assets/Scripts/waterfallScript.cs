using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterfallScript : MonoBehaviour {

    float x;
    float start_y;
    float z;

	// Use this for initialization
	void Start () {
        x = transform.position.x;
        z = transform.position.z;
        start_y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(x, 20, z);
        }

		
	}
}
