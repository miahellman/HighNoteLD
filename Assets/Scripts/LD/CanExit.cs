using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanExit : MonoBehaviour
{
    public bool canExit = false;

    public void OnTriggerEnter(Collider other)
    {
        //if player touching exit door, can exit
        if (other.gameObject.tag == "BarExit")
        {
            Debug.Log("can exit");
            canExit = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BarExit")
        {
            canExit = false;
        }
    }
}
