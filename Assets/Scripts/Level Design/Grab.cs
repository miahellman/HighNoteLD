using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab: MonoBehaviour //https://www.youtube.com/watch?v=6bFCQqabfzo&list=WL&index=7&t=20s
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    [SerializeField] Drink drink; 

    GameObject heldObj; 
    Rigidbody heldBody;

    [Header("Physics Parameters")]
    [SerializeField] float pickupRange = 5f;
    [SerializeField] float pickupForce = 150f;
    [SerializeField] float throwForce = 1f; //impulse force for throwing objects

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(heldObj == null)
            {
                RaycastHit hit; 
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    // Pickup Object
                    PickupObject(hit.transform.gameObject);
                    if (hit.transform.gameObject.tag == "Drink") //if the object is drink object
                    {
                        drink.drinkObject = hit.transform.gameObject; //assign it so that drink script can access it
                    }
                }
            } 
            else
            {
                DropObject();
                drink.drinkObject = null;
            }
        }
        if (heldObj != null)
        {
            MoveObject();
        }

        //able to throw if holding an object
        if(Input.GetMouseButtonDown(1) && heldObj != null)
        {
            ThrowObject();
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > .1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position); 
            heldBody.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>()) 
        {
            heldBody = pickObj.GetComponent<Rigidbody>();
            heldBody.useGravity = false;
            heldBody.drag = 10;
            heldBody.constraints = RigidbodyConstraints.FreezeRotation;

            heldBody.transform.parent = holdArea; 
            heldObj = pickObj;
        }
    }
    void DropObject()
    {
        heldBody.useGravity = true;
        heldBody.drag = 1;
        heldBody.constraints = RigidbodyConstraints.None;

        heldBody.transform.parent = null;
        heldObj = null; 
    }

    //throwing objects with right click
    void ThrowObject()
    {
        //update settings for the object being held
        heldBody.useGravity = true;
        heldBody.drag = 1;
        heldBody.constraints = RigidbodyConstraints.None;

        heldBody.transform.parent = null;
        heldObj = null; 

        //add impulse to the object to throw
        heldBody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }
}
