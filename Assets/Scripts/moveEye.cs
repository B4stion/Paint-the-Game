using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveEye : MonoBehaviour {

    public bool rotateSwitch;
    public float rotateTime;

    private void Start()
    {
        rotateSwitch = true;
        rotateTime = 10;
    }

    // Update is called once per frame
    void Update() {
        if (rotateSwitch)
        {
            //transform.Rotate(Vector3.right * Time.deltaTime * rotateTime);
            transform.RotateAround(transform.position, transform.right, 10 * Time.deltaTime);
        }
        else if (!rotateSwitch)
        {
            //transform.Rotate(-Vector3.right * Time.deltaTime * rotateTime);
            transform.RotateAround(transform.position, -transform.right, 10 * Time.deltaTime);
        }
        //print(transform.rotation.x);
        if (transform.rotation.x < .18)
        {
            rotateSwitch = true;
        }
        else if (transform.rotation.x > .45)
        {
            rotateSwitch = false;
        }
	}
}
