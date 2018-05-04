using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globalVariableHolder {

    public static bool canPurple = false;
    public static bool canYellow = false;
    public static bool canTeal = false;

    public static void truePurple()
    {
        canPurple = true;
    }
    public static void trueYellow()
    {
        canYellow = true;
    }
    public static void trueTeal()
    {
        canTeal = true;
    }

    public static GUIStyle NormalStyle()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.fontSize = 40;

        return style;
    }
    public static GUIStyle BounceStyle()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontSize = 40;

        return style;
    }
    public static GUIStyle SlideStyle()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.blue;
        style.fontSize = 40;

        return style;
    }
    public static GUIStyle FloatStyle()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.green;
        style.fontSize = 40;

        return style;
    }
    public static GUIStyle BounceSlideStyle()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = new Color(255, 0, 255);
        style.fontSize = 40;

        return style;
    }
    public static GUIStyle BounceFloatStyle()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = new Color(255, 255, 0);
        style.fontSize = 40;

        return style;
    }
    public static GUIStyle SlideFloatStyle()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = new Color(0, 255, 255);
        style.fontSize = 40;

        return style;
    }
    public static GUIStyle EraseStyle()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.grey;
        style.fontSize = 40;

        return style;
    }
}
