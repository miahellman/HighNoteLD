using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UrineManager : MonoBehaviour
{
    //CRITICAL BUG WHERE THE PLAYER'S LOCATION NEEDS TO BE RESET MOVING IN BETWEEN SCENES, AND THE PLAYER OBJECT NEEDS TO DELETE THE "OTHER" PLAYER WHEN IT ISN'T DESTROYED ON LOAD
    //locations should reset to where the entrances/exits are on entering a scene

    public FOVChange fovChange;

    //game manager
    public GameManager gameManager;

    public float urineMeter;
    public float urineMax;
    //public bool peed;
    public bool canPee;

    //pee visual
    [SerializeField] GameObject peeObject;

    //[SerializeField] bool canEnterBathroom, canExitBathroom;

    //public Transform enterPosition; 
    //public Transform exitPosition;  

    private void Start()
    {
        //find game manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        peeObject = GameObject.Find("Pee");

        urineMeter = 0f; 
    }
    private void Update()
    {
       /*
        if (SceneManager.GetActiveScene().name == "Bathroom")
        {
            exitPosition = GameObject.Find("ExitPosition").GetComponent<Transform>();
        }
        if (SceneManager.GetActiveScene().name == "InsideScene")
        {
            enterPosition = GameObject.Find("EnterPosition").GetComponent<Transform>();
        }
       */
    }
        
    private void FixedUpdate()
    {
        //VISUALIZATION OF URINE MANAGER SOMEWHERE HERE

        /*
        if (Input.GetKey(KeyCode.E) && canEnterBathroom)
        {
            transform.position = enterPosition.position;
            gameManager.EnterBathroom();  

            canEnterBathroom = false;
        }

        if (Input.GetKey(KeyCode.E) && canExitBathroom)
        {
            transform.position = exitPosition.position;
            gameManager.ExitBathroom(); 

            canExitBathroom = false;
        }
        */

        PeeingInBathroom();
    }

    public void PeeingInBathroom() //maybe there's a peeing out of the bathroom option if you get really drunk
    {
        if (canPee && Input.GetKey(KeyCode.E)) //if they can pee and they hit the pee button
        {
            //reg pee
            if (urineMeter > 0f)
            {
                peeObject.SetActive(true);
                urineMeter--;
            } 
            else
            {
                urineMeter = 0f; 
            }
            fovChange.resetFOV();
        } 
        else
        {
            //pee object set inactive if not peeing
            peeObject.SetActive(false);
        }
        
    }

    public void TooMuchUrine()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if in range to pee
        if (other.tag == "Toilet")
        {
            canPee = true;
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        

        //if out range to pee
        if (other.tag == "Toilet")
        {
            canPee = false;
        }


    }
}
