using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeRB : MonoBehaviour {
	private bool hasRB = true;

    public float length;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hasRB)
        {
            if (GetComponent<Rigidbody>().useGravity == false)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                Destroy(rb);
                hasRB = false;
            }
        }
	}

    // When colliding with ground, remove rigidbody so platform is final
	void OnCollisionEnter(Collision coll)
    {
        // 
		if ("Floor" == coll.gameObject.tag && hasRB)
        {
			// Remove rigidbody so platform stays in place
			Rigidbody rb = GetComponent<Rigidbody>();
			Destroy(rb);
            hasRB = false;
        }
    }
}
