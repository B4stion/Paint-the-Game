using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour {

    public string ability;
    public Color currentColor;
    public float addAmount;

	// Use this for initialization
	void Start () {

        if (ability == "bounce")
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if(ability == "slide")
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (ability == "floating")
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (ability == "bounce_slide")
        {
            GetComponent<Renderer>().material.color = new Color(255, 0, 255);
        }
        else if (ability == "bounce_floating")
        {
            GetComponent<Renderer>().material.color = new Color(255, 255, 0);
        }
        else if (ability == "slide_floating")
        {
            GetComponent<Renderer>().material.color = new Color(0, 255, 255);
        }
        else if (ability == "normal")
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            if (ability == "bounce")
            {
                Camera.main.GetComponent<painting>().current_bounce += addAmount;
                Camera.main.GetComponent<painting>().total_bounce += addAmount;
            }
            else if (ability == "slide")
            {
                Camera.main.GetComponent<painting>().current_slide += addAmount;
                Camera.main.GetComponent<painting>().total_slide += addAmount;
            }
            else if (ability == "floating")
            {
                Camera.main.GetComponent<painting>().current_floating += addAmount;
                Camera.main.GetComponent<painting>().total_floating += addAmount;
            }
            else if (ability == "bounce_slide")
            {
                Camera.main.GetComponent<painting>().current_bounce += addAmount;
                Camera.main.GetComponent<painting>().total_bounce += addAmount;
                Camera.main.GetComponent<painting>().current_slide += addAmount;
                Camera.main.GetComponent<painting>().total_slide += addAmount;
            }
            else if (ability == "bounce_floating")
            {
                Camera.main.GetComponent<painting>().current_bounce += addAmount;
                Camera.main.GetComponent<painting>().total_bounce += addAmount;
                Camera.main.GetComponent<painting>().current_floating += addAmount;
                Camera.main.GetComponent<painting>().total_floating += addAmount;
            }
            else if (ability == "slide_floating")
            {
                Camera.main.GetComponent<painting>().current_slide += addAmount;
                Camera.main.GetComponent<painting>().total_slide += addAmount;
                Camera.main.GetComponent<painting>().current_floating += addAmount;
                Camera.main.GetComponent<painting>().total_floating += addAmount;
            }
            else if (ability == "normal")
            {
                Camera.main.GetComponent<painting>().current_normal += addAmount;
                Camera.main.GetComponent<painting>().total_normal += addAmount;
            }
            Destroy(this);
            Destroy(gameObject);
        }
    }
}
