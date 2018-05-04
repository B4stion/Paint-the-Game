using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class GUIElements : MonoBehaviour {

	string display_text, normal_text, bounce_text, slide_text, floating_text, erase_text;
    float current_normal, current_bounce, current_slide, current_floating, total_normal, total_bounce, total_slide, total_floating;
    public GUIStyle style;
    public GUIStyle instruction_style;
    string instruction;

    private bool toggle;

    // Use this for initialization
    void Start ()
    {
        display_text = "";
        normal_text = "";
        bounce_text = "";
        slide_text = "";
        floating_text = "";
        erase_text = "";
        style.normal.textColor = Color.black;
        style.fontSize = 40;

        /*mainStyle.normal.textColor = Color.black;
        mainStyle.fontSize = 40;
        bounceStyle.normal.textColor = Color.red;
        bounceStyle.fontSize = 40;
        slideStyle.normal.textColor = Color.blue;
        slideStyle.fontSize = 40;
        floatStyle.normal.textColor = Color.green;
        floatStyle.fontSize = 40;*/

        instruction_style.normal.textColor = Color.white;
        instruction_style.fontSize = 20;
        toggle = false;
        instruction = "You need to get to the red glowing door on the wall.\nUse WASD to move, SPACEBAR to jump.\nMove mouse to change the brush position.\nHold right mouse button and move mouse to look around.\nUse Z and X keys to toggles back and forth through different paint types.\nUse the mouse wheel to change the distance of the paint brush.\nUse E Key to grab and ungrab white objects.\nWhile grabbingthe object, use Q key to throw them.\nUse Esc for reset.\n\nOnce you obtain special mesh item, use left mouse button click while holding H key \nin order to paint special platform, but it will use a lot of paint.\nPaint will fall from the position it’s drawn until it hits the ground.\nDifferent color paints have different properties.\n\nGo around and collect colored cylinders to acquire paint.";
    }

    // Update is called once per frame
    void FixedUpdate () {

        display_text = painting.paintScript.current_kind_string;
        current_normal = (float) System.Math.Round(painting.paintScript.current_normal, 2);
        current_bounce = (float)System.Math.Round(painting.paintScript.current_bounce, 2);
        current_slide = (float)System.Math.Round(painting.paintScript.current_slide, 2);
        current_floating = (float)System.Math.Round(painting.paintScript.current_floating, 2);

        /*if(display_text == "Normal")
        {
            mainStyle.normal.textColor = Color.black;
        }
        if (display_text == "Bounce")
        {
            mainStyle.normal.textColor = Color.red;
        }
        if (display_text == "Slide")
        {
            mainStyle.normal.textColor = Color.blue;
        }
        if (display_text == "Floating")
        {
            mainStyle.normal.textColor = Color.green;
        }
        if (display_text == "Bounce&Slide")
        {
            mainStyle.normal.textColor = new Color(255, 0, 255);
        }
        if (display_text == "Bounce&Floating")
        {
            mainStyle.normal.textColor = new Color(255, 255, 0);
        }
        if (display_text == "Slide&Floating")
        {
            mainStyle.normal.textColor = new Color(0, 255, 255);
        }
        if (display_text == "Erase")
        {
            mainStyle.normal.textColor = Color.grey;
        }*/

        //print(current_normal);

        if (Input.GetKey(KeyCode.Backspace))
        {
            toggle = true;
        }
        else
        {
            toggle = false;
        }        
    }




	private void OnGUI()
	{
        GUIStyle mainStyle = globalVariableHolder.NormalStyle();

        if (display_text == "Normal")
        {
            mainStyle = globalVariableHolder.NormalStyle();
        }
        if (display_text == "Bounce")
        {
            mainStyle = globalVariableHolder.BounceStyle();
        }
        if (display_text == "Slide")
        {
            mainStyle = globalVariableHolder.SlideStyle();
        }
        if (display_text == "Floating")
        {
            mainStyle = globalVariableHolder.FloatStyle();
        }
        if (display_text == "Bounce&Slide")
        {
            mainStyle = globalVariableHolder.BounceSlideStyle();
        }
        if (display_text == "Bounce&Floating")
        {
            mainStyle = globalVariableHolder.BounceFloatStyle();
        }
        if (display_text == "Slide&Floating")
        {
            mainStyle = globalVariableHolder.SlideFloatStyle();
        }
        if (display_text == "Erase")
        {
            mainStyle = globalVariableHolder.EraseStyle();
        }
 
        GUIStyle bounceStyle = globalVariableHolder.BounceStyle();
        GUIStyle slideStyle = globalVariableHolder.SlideStyle();
        GUIStyle floatStyle = globalVariableHolder.FloatStyle();


        if (!toggle)
        {
            /*GUI.Label(new Rect(10, 10, 200, 40), "Type: " + display_text, style);
            GUI.Label(new Rect(10, 50, 200, 40), "Normal: "+current_normal.ToString(), style);
            GUI.Label(new Rect(10, 90, 200, 40), "Bounce: "+current_bounce.ToString(), style);
            GUI.Label(new Rect(10, 130, 200, 40), "Slide: "+current_slide.ToString(), style);
            GUI.Label(new Rect(10, 170, 200, 40), "Floating: "+current_floating.ToString(), style);
            GUI.Label(new Rect(10, 220, 200, 40), "Press Backspace for instruction", style);*/

            
            GUI.Label(new Rect(10, 10, 200, 40), "Type: " + display_text, mainStyle);
            GUI.Label(new Rect(10, 50, 200, 40), "Normal: " + current_normal.ToString(), style);
            GUI.Label(new Rect(10, 90, 200, 40), "Bounce: " + current_bounce.ToString(), bounceStyle);
            GUI.Label(new Rect(10, 130, 200, 40), "Slide: " + current_slide.ToString(), slideStyle);
            GUI.Label(new Rect(10, 170, 200, 40), "Floating: " + current_floating.ToString(), floatStyle);
            GUI.Label(new Rect(10, 220, 200, 40), "Press Backspace \nfor instruction", style);
        }
        else
        {
            GUI.Label(new Rect(500, 250, 1000, 2000), instruction, instruction_style);
        }
    }
}