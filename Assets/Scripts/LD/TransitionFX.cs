using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFX : MonoBehaviour
{
    /// <summary>
    /// serialize player collision code here so when collision is called we can set a bool to true to trigger the effect
    /// </summary>

    //serialize the vignette material here
    [SerializeField] Material vignetteMat;

    //shader values to modify
    string vRadius = "_vr";

    //use these floats to modify radius in the shader
    float maxRadius = 0.864f;
    float minRadius = 0f;
    float currentRadius;

    //door opened bool - hidden in inspector 
    //use this to trigger - bool is public so we can call it from player collisions
    [HideInInspector] public bool doorOpened = false;

    // Start is called before the first frame update
    private void Start()
    {
        //get material componenet
        vignetteMat = GetComponent<TransitionFX>().vignetteMat;
        //return errors if missing property
        if (!vignetteMat.HasProperty(vRadius)){Debug.LogError("the shader associated with the material on this game object is missing a necessary property. _vr is required");}
    }

    //update the shader values in update functions
    private void Update()
    {
        //when door is opened set radius to max radius and softness to max softness
        if(doorOpened)
        {
            //shrink vignette radius when door is opened so screen is not visible
            if (currentRadius <= minRadius){currentRadius = minRadius;}
            else{currentRadius -= 0.0005f;}
        }
        else //when door != set to open set radius to min radius and softness to min softness
        {
            //add to vignette radius when door is not opened so screen is visible
            if (currentRadius >= maxRadius) { currentRadius = maxRadius; }
            else { currentRadius += 0.0005f; }
        }

        //replace old value with new modified values in the shader
        vignetteMat.SetFloat(vRadius, currentRadius);

    }
}
