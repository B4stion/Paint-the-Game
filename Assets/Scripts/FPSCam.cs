using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour {

    // Variables Used for camera position and orientation
	private GameObject orient;
    public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		
		// Find Player object and orient
		player = GameObject.Find("Player");
		orient = GameObject.Find("PlayerOrient");

		// Make player opaque to see brush
		Color color = player.GetComponent<Renderer>().material.color;

        var standardShaderMaterial1 = player.GetComponent<Renderer>().material;
        standardShaderMaterial1.color = new Color(color.r, color.g, color.b, 0.5f);
        standardShaderMaterial1.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        standardShaderMaterial1.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        standardShaderMaterial1.SetInt("_ZWrite", 0);
        standardShaderMaterial1.DisableKeyword("_ALPHATEST_ON");
        standardShaderMaterial1.DisableKeyword("_ALPHABLEND_ON");
        standardShaderMaterial1.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        standardShaderMaterial1.renderQueue = 3000;

        transform.rotation = player.transform.rotation;

		//Initialize camera offset
		offset = (new Vector3(0, 0.5f, 0)) - player.transform.forward*3;
        transform.position = player.transform.position + offset;
	}

	// LateUpdate is called once all scene objects are in final position
	void LateUpdate()
	{
        // Update position of camera to point at player
        /*
        offset = (new Vector3(0,0.5f,0)) -orient.transform.forward*3;
		transform.position = orient.transform.position + offset;
        transform.rotation = orient.transform.rotation;
        print("PlayerOrient: " + orient.transform.rotation);
        print("Camera: " + transform.rotation);
        transform.LookAt(orient.transform.rotation);
        transform.forward = -orient.transform.forward;
        */
        
		// Update angle of camera if player wants to look around
        /*
		int rightClick = 1;
		if (Input.GetMouseButton(rightClick)) {
			yaw += speedH * Input.GetAxis("Mouse X");
			pitch -= speedV * Input.GetAxis("Mouse Y");
			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		}
        */
	}
}
