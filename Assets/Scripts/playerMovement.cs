// Authors: Griffin Shaw﻿
// Details: Controls Player Movement
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour {
	// Object to orient movement and camera
	private GameObject orient;
    //Used to fix movement bug when looking up or down
    private GameObject xPlane;

    // Movement related variables
	private float RotateSpeed = 100f;
	private Rigidbody rb;
	private bool canJump;
	private float thrust;
	private float maxV = 5f;
    private Vector3 offset;

    public float speedH = 4.0f;
    public float speedV = 4.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Racasts used for Jump Detection
    private Ray groundRay1, groundRay2, groundRay3, groundRay4, groundRay5;
    private RaycastHit hit1, hit2, hit3, hit4, hit5;

    private bool standOnSlide;

    float sensitivity = 0.05f;

    // Use this for initialization
    void Start () {
        //Cursor.lockState = CursorLockMode.Locked;

		// Create child orient at player poisition
		//orient = new GameObject("PlayerOrient");
		orient.transform.position = transform.position;
		orient.transform.rotation = transform.rotation;
		orient.transform.parent = transform;

        //Initially set xPlane to same as orient
        xPlane.transform.position = transform.position;
        xPlane.transform.rotation = transform.rotation;
        xPlane.transform.parent = transform;

        // Constrain player rotation
        rb = GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Initialize jump to false
		canJump = false;
        //Initialize standing on slide to false
        standOnSlide = false;

        offset = (new Vector3(0, 0.5f, 0)) - orient.transform.forward * 3;
        Camera.main.transform.position = orient.transform.position + offset;

        //Set Camera start angle
        pitch = Camera.main.transform.eulerAngles.x;
        yaw = Camera.main.transform.eulerAngles.y;

    }
    // function that checks whether the player can jump at the given moment

    //Happens right before start
    private void Awake()
    {
        orient = new GameObject("PlayerOrient");
        xPlane = new GameObject("xPlane");
    }

    void updateCanJump()
    {
		canJump = isGrounded();
    }

    //function that checks whether the player is on the ground
    bool isGrounded()
    {
        // we have conduct raycast on 5 points.
        bool result1 = Physics.Raycast(groundRay1, out hit1, 1.015f); // on x-axis left 
        bool result2 = Physics.Raycast(groundRay2, out hit2, 1.015f); // on x-axis right
        bool result3 = Physics.Raycast(groundRay3, out hit3, 1.015f); // on the center
        bool result4 = Physics.Raycast(groundRay4, out hit4, 1.015f); // on z-axis left
        bool result5 = Physics.Raycast(groundRay5, out hit5, 1.015f); // on z-axis right

        bool standOnFloor = false;

        // if at least one of the raycast detects a floor, we consider the player to be standing on the floor
        if ((result1 || result2) || ((result3 || result4) || result5))
        {
            if (result1 && (hit1.collider.tag == "Floor" || hit1.collider.tag == "Paint"))
            {
                standOnFloor = true;

                if (hit1.collider.material.staticFriction == 0)
                {
                    standOnSlide = true;
                }
                else
                {
                    standOnSlide = false;
                }
            }
            else if (result2 && (hit2.collider.tag == "Floor" || hit2.collider.tag == "Paint"))
            {
                standOnFloor = true;

                if (hit2.collider.material.staticFriction == 0)
                {
                    standOnSlide = true;
                }
                else
                {
                    standOnSlide = false;
                }
            }
            else if (result3 && (hit3.collider.tag == "Floor" || hit3.collider.tag == "Paint"))
            {
                standOnFloor = true;

                if (hit3.collider.material.staticFriction == 0)
                {
                    standOnSlide = true;
                }
                else
                {
                    standOnSlide = false;
                }
            }
            else if (result4 && (hit4.collider.tag == "Floor" || hit4.collider.tag == "Paint"))
            {
                standOnFloor = true;

                if (hit4.collider.material.staticFriction == 0)
                {
                    standOnSlide = true;
                }
                else
                {
                    standOnSlide = false;
                }
            }
            else if (result5 && (hit5.collider.tag == "Floor" || hit5.collider.tag == "Paint"))
            {
                standOnFloor = true;

                if (hit5.collider.material.staticFriction == 0)
                {
                    standOnSlide = true;
                }
                else
                {
                    standOnSlide = false;
                }
            }
            else
            {
                standOnFloor = false;
                standOnSlide = false;
            }

            return standOnFloor;
        }
        else
        {
            standOnSlide = false;
            return false;
        }
    }


        // FixedUpdate is called once per frame
    void FixedUpdate ()
    {
        // Update player rotation 

        /*
        if (!lookAround && Input.GetKey("q")) {
			orient.transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
            Camera.main.transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
        }
		else if (!lookAround && Input.GetKey("e")) {
            orient.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
            Camera.main.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
        }
        */

        float xPercent = Input.mousePosition.x / Screen.width;
        float yPercent = Input.mousePosition.y / Screen.height;

        // print(xPercent + "/" + yPercent);

        //activate the following code block if you want to change looking around portion of the program
        /*
        if ((xPercent<0.2||xPercent>0.8)||(yPercent<0.2||yPercent>0.8))

        {
            Vector3 vp = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            vp.x -= 0.5f;
            vp.y -= 0.5f;
            vp.x *= sensitivity;
            vp.y *= sensitivity;
            vp.x += 0.5f;
            vp.y += 0.5f;
            Vector3 sp = Camera.main.ViewportToScreenPoint(vp);

            Vector3 v = Camera.main.ScreenToWorldPoint(sp);
            //Camera.main.transform.Rotate(v);
            Camera.main.transform.LookAt(v, Vector3.up);
            orient.transform.rotation = Camera.main.transform.rotation;
            //orient.transform.LookAt(v, Vector3.up);

        }
        */

        
        if (Input.GetMouseButton(1))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            Camera.main.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            orient.transform.rotation = Camera.main.transform.rotation;
            xPlane.transform.rotation = Quaternion.Euler(0.0f, Camera.main.transform.eulerAngles.y, 0.0f);
        }
        

        
        offset = (new Vector3(0, 0.5f, 0)) - orient.transform.forward * 3;
        Camera.main.transform.position = orient.transform.position + offset;

        // we update the ray based on the current position of the player
        groundRay1 = new Ray(gameObject.transform.position - new Vector3(0.5f, 0, 0), Vector3.down);
        groundRay2 = new Ray(gameObject.transform.position + new Vector3(0.5f, 0, 0), Vector3.down);
        groundRay3 = new Ray(gameObject.transform.position, Vector3.down);
        groundRay4 = new Ray(gameObject.transform.position - new Vector3(0, 0, 0.5f), Vector3.down);
        groundRay5 = new Ray(gameObject.transform.position + new Vector3(0, 0, 0.5f), Vector3.down);

        updateCanJump();
        // Check if any horizontal velocity component is at max
        // Zero force addition in that direcion
        Vector3 limiter = new Vector3(1,1,1);
		if (Mathf.Abs(rb.velocity.x) >= maxV) {
			limiter.x = 0;
		}
		if (Mathf.Abs(rb.velocity.z) >= maxV) {
			limiter.z = 0;
		}

        int coefficient;

        if (standOnSlide)
        {
            coefficient = 4;
            //print("Lslsls");
        }
        else
        {
            coefficient = 1;
        }

		// Add forces in corresponding directions
		if (Input.GetKey ("d")) {
			Vector3 right = orient.transform.right * 10;
			// Apply limiter to force
			right = Limit (right, limiter);
            rb.AddForce (coefficient*right);
		}
		if (Input.GetKey ("a")) {
			Vector3 left = -orient.transform.right * 10;
			left = Limit (left, limiter);
            rb.AddForce (coefficient * left);
		}
		if (Input.GetKey ("w")) {

            //Vector3 forward = orient.transform.forward * 10;
            Vector3 forward = xPlane.transform.forward * 10;

            //print("Forward: " + forward);
            //print("Euler Angles: " + orient.transform.eulerAngles);

            forward = Limit (forward, limiter);
            rb.AddForce (coefficient * forward);
		}
		if (Input.GetKey ("s")) {
			Vector3 back = -xPlane.transform.forward * 10;
            back = Limit (back, limiter);
            rb.AddForce (coefficient * back);
		}
		if (Input.GetKey ("space") && canJump) {
			Vector3 up = new Vector3 (0, 5, 0);
			rb.velocity = up;
		}
	}

	Vector3 Limit(Vector3 force, Vector3 limiter) {
		// Keep players from flying
		force.y = 0;

		// Limit speed based on vector components
		// Only limit forces in the same direction as max speed
		if ( (rb.velocity.x > 0 && force.x > 0) || (rb.velocity.x < 0 && force.x < 0)) {
			force.x *= limiter.x;
		}
		if ( (rb.velocity.z > 0 && force.z > 0) || (rb.velocity.z < 0 && force.z < 0)) {
			force.z *= limiter.z;
		}

		return force;

	}

    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.name);
        if (collision.gameObject.tag == "MeshItem")
        {
            Camera.main.GetComponent<painting>().specialMesh = collision.gameObject.GetComponent<MeshFilter>().mesh;
            Camera.main.GetComponent<painting>().specialScale = collision.gameObject.transform.localScale;
            Destroy(collision.gameObject);
        }
    }
}
