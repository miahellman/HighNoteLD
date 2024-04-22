using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanExit : MonoBehaviour
{
    public bool canExit = false;


    private void OnCollisionEnter(Collision collision)
    {
        //if player touching exit door, can exit
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Body")
        {
            Debug.Log("can exit");
            canExit = true;
        } else { canExit = false; }
    }
}
