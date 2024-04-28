using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.Port;

public class FOVChange : MonoBehaviour
{
    public Camera cam;

    [Header("FOV")]
    public float fovMax; 
    public float fovModifier;

    [Header("Pee")]
    public Image overlay;
    public float peeAlpha = 0f;
    public float peeAlphaMax = 60f;

    public void increaseFOV()
    {
        if (cam.fieldOfView <= fovMax /* && (cam.fieldOfView!=cam.fieldOfView + fovModifier) <- the problem here was that it didn't get enough time to add 10 each time*/) 
        {
            //cam.fieldOfView ++; 
            cam.fieldOfView += fovModifier; 
        }

        if (peeAlpha <= peeAlphaMax)
        {
            peeAlpha += fovModifier/10f;
        }

    }

    public void resetFOV()
    {
        if (cam.fieldOfView > 60f)
        {
            cam.fieldOfView--;
        }
        else { cam.fieldOfView = 60f; }

        if (peeAlpha > 0f)
        {
            peeAlpha--;
        } else
        {
            peeAlpha = 0f;
        }
    }
    private void Update()
    {
        overlay.color = new Color(255, 255, 0, peeAlpha);
    }
}
