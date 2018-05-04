//Authors: Scott Hodnefield
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintMixerButtons : MonoBehaviour {

    painting paintingScript;
    GameObject buttonPurple;
    GameObject buttonYellow;
    GameObject buttonTeal;
    float radius;

    // Use this for initialization
    void Start () {
        paintingScript = GameObject.Find("Main Camera").GetComponent<painting>();
        radius = 2;

        buttonPurple = GameObject.Find("buttonPurple");
        buttonYellow = GameObject.Find("buttonYellow");
        buttonTeal = GameObject.Find("buttonTeal");
    }
	
	// Update is called once per frame
	void Update () {
        //Change color of paint mixer buttons to indicate which paints are available
        if (globalVariableHolder.canPurple == true)
        {
            GameObject.Find("buttonPurple").GetComponent<Renderer>().material.color = new Color(255, 0, 255);
        }
        if (globalVariableHolder.canYellow == true)
        {
            GameObject.Find("buttonYellow").GetComponent<Renderer>().material.color = new Color(255, 255, 0);
        }
        if (globalVariableHolder.canTeal == true)
        {
            GameObject.Find("buttonTeal").GetComponent<Renderer>().material.color = new Color(0, 255, 255);
        }

        //Give access to mixed paints when player brings an object mark [Color]Key near the paint mixer
        //Key objects should be tagged moveable
        Collider[] keyChecks = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < keyChecks.Length)
        {
            if (keyChecks[i].name == "PurpleKey")
            {
                GivePurple();
                Destroy(keyChecks[i].gameObject);
            }
            else if (keyChecks[i].name == "YellowKey")
            {
                GiveYellow();
                Destroy(keyChecks[i].gameObject);
            }
            else if (keyChecks[i].name == "TealKey")
            {
                GiveTeal();
                Destroy(keyChecks[i].gameObject);
            }
            i++;
        }


        /*if (Input.GetKeyDown(KeyCode.T))
        {
            GiveTeal();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GiveYellow();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GivePurple();
        }*/

    }

    void GivePurple()
    {
        globalVariableHolder.truePurple();
    }

    void GiveYellow()
    {
        globalVariableHolder.trueYellow();
    }

    void GiveTeal()
    {
        globalVariableHolder.trueTeal();
    }
}
