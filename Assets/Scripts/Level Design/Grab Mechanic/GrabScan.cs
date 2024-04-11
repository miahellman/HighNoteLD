using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabScan : MonoBehaviour
{

    // NOTES FOR CHASE LATER: Mathf.Infinity is probably the reason why the "hit cast" goes forever
    [Header("Other Scripts")]
    public SwitchingBodies sb; 
    public PlayerMovement pm;
    public Drink drink; 

    [Header("Internal Constraints")]
    [SerializeField] LayerMask grabScanLayerMask; //this is used to select which layers the hitscan will interact with (other layers will be ignored)
    [SerializeField] GameObject grabScanLineVisual; //the visual representation of the hitscan line that will be instantiated when the player triggers the hitscan action
    [SerializeField] float aimAssistLevel = 2f; //the radius of the spherecast that will be used to assist the player in hitting targets

    PlayerInput playerInput;
    Transform cameraHolder; //get the cameraHolder so we can start the hitscan from the camera's position and direction (rotation)
    bool grabScanTriggered; //set to true for the frame when the player triggers the hitscan action



    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cameraHolder = transform.Find("CameraHolder"); //finds a child of the player object called CameraHolder
    }

    // Update is called once per frame
    void Update()
    {

        grabScanTriggered = playerInput.actions["GrabScan"].triggered; //check if the player has triggered the hitscan action - LEFT MOUSE BUTTON

        if(grabScanTriggered)
        { 
            //if the player has triggered the scan action, start the hitscan
            //we will now make an object to show the player where the cast is.
            Instantiate(grabScanLineVisual, cameraHolder.position + (cameraHolder.forward * 150) - (cameraHolder.up * .5f), cameraHolder.rotation, null);
            if(GrabScanRayCast(cameraHolder.position, cameraHolder.forward) == false)
            {
                GrabScanSphereCast(cameraHolder.position, cameraHolder.forward);
            }
        }
    }

    bool GrabScanRayCast(Vector3 origin, Vector3 direction) // Check if the raycast hits an object. If it does, evaluate the object hit.
    {
        if(direction == null) direction = cameraHolder.forward; //if the direction is null, set it to the forward direction of the cameraHolder
        RaycastHit hit; //this is the object that will store information about what the raycast hit
        if(Physics.Raycast(origin, direction, out hit, Mathf.Infinity, grabScanLayerMask))
        { 
            return GrabScanEvaluateObject(hit); //if the raycast hit something, evaluate the object hit. Only if this returns true will the cast also return true
        }
        return false; //if the raycast didn't hit an object that interests us, return false
    }

    bool GrabScanSphereCast(Vector3 origin, Vector3 direction) // Check if spherecast hits an object. If it does, evaluate the object hit. (i believe this is aim assist)
    {
        if (direction == null) direction = cameraHolder.forward; //if the direction is null, set it to the forward direction of the cameraHolder
        RaycastHit hit; //this is the object that will store information about what the raycast hit
        if(Physics.SphereCast(origin, aimAssistLevel, direction, out hit, Mathf.Infinity, grabScanLayerMask))
        { 
            return GrabScanEvaluateObject(hit); //if the spherecast hit something, evaluate the object hit. Only if evaluation returns true will the cast also return true
        }
        return false; //otherwise false
    }
    bool GrabScanEvaluateObject(RaycastHit hit) 
    {


        /* //GameFeel Unused
        if(hit.collider.gameObject.tag == "Body"){ 
            sb.targetName = hit.collider.gameObject.name;
            pm.switchingBody = true; 
            return true; //return true if the raycast hit an object that interests us
        }
        */


        #region Grabbing Beer
        if (hit.collider.gameObject.tag == "Drink") //if the object hit has the tag "Drink" do this:
        {
            //Grab Beer Trigger Here
            //drink.grabbingBeer = true; 
            return true; //true if the object is drink
        }
        #endregion

        return false; //false if none of these objects
    }
}
