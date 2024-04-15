using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVChange : MonoBehaviour
{
    public Camera cam;
    public float fovMax; 
    public float fovModifier;

    public void increaseFOV()
    {
        if (cam.fieldOfView <= fovMax /* && (cam.fieldOfView!=cam.fieldOfView + fovModifier) <- the problem here was that it didn't get enough time to add 10 each time*/) 
        {
            //cam.fieldOfView ++; 
            cam.fieldOfView += fovModifier; 
           
        }

    }

    public void resetFOV()
    {
        if (cam.fieldOfView > 60f)
        {
            cam.fieldOfView--;
        }
        else { cam.fieldOfView = 60f; }
    }
}
