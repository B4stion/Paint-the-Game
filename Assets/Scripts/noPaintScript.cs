using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noPaintScript : MonoBehaviour {

    float radius;
    painting paintingScript;
	// Use this for initialization
	void Start () {
        radius = 10;
        paintingScript = GameObject.Find("Player").GetComponent<painting>();
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] deletables = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < deletables.Length)
        {
            if (deletables[i].tag == "Paint")
            {
                print("Paint");
                float recovered_paint = deletables[i].GetComponent<removeRB>().length;
                Color recovered_color = deletables[i].GetComponent<MeshRenderer>().material.color;
                //ReturnPaint(recovered_paint, recovered_color);
                Destroy(deletables[i].gameObject);
            }
            i++;
        }
    }

    void ReturnPaint(float length, Color color)
    {
        if (color == Color.black)
        {
            paintingScript.current_normal += length;
        }
        else if (color == Color.red)
        {
            paintingScript.current_bounce += length;
        }
        else if (color == Color.blue)
        {
            paintingScript.current_slide += length;
        }
        else if (color == Color.green)
        {
            paintingScript.current_floating += length;
        }
        else if (color == new Color(255, 0, 255))
        {
            paintingScript.current_slide += length;
            paintingScript.current_bounce += length;
        }
        else if (color == new Color(255, 255, 0))
        {
            paintingScript.current_floating += length;
            paintingScript.current_bounce += length;
        }
        else if (color == new Color(0, 255, 255))
        {
            paintingScript.current_slide += length;
            paintingScript.current_floating += length;
        }
        else
        {

        }
    }
}
