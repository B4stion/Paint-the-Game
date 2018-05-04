using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signScript : MonoBehaviour {

    bool displayText;
    float radius = 3;
    string signText;
    public GUIStyle signStyle;

	// Use this for initialization
	void Start () {

        signText = "Careful, the acid cloud \nwill dissolve your paint!";
        signStyle.normal.textColor = Color.white;
        signStyle.fontSize = 25;
		
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] deletables = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        int j = 0;
        while (i < deletables.Length)
        {
            if (deletables[i].tag == "Player")
            {
                displayText = true;
            }
            else
            {
                j++;
            }
            i++;
        }
        if (i == j)
        {
            displayText = false;
        }
    }

    private void OnGUI()
    {
        if (displayText)
        {
            //GUI.Label(new Rect(500, 250, 1000, 2000), signText, signStyle);
            GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 25, 100, 50), signText, signStyle);
        }
        else
        {
            
        }
    }
}
