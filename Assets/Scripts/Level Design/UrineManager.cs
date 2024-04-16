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

    bool canEnterBathroom;
    bool canExitBathroom;

    private void Start()
    {
        //find game manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        urineMeter = 0f; 
    }
    private void Update()
    {
        //VISUALIZATION OF URINE MANAGER SOMEWHERE HERE

        if (Input.GetKey(KeyCode.E) && canEnterBathroom)
        {
            gameManager.EnterBathroom();
        }

        if (Input.GetKey(KeyCode.E) && canExitBathroom)
        {
            gameManager.ExitBathroom();
        }

        PeeingInBathroom();
    }

    public void PeeingInBathroom() //maybe there's a peeing out of the bathroom option if you get really drunk
    {
        if (canPee && Input.GetKeyDown(KeyCode.E)) //if they can pee and they hit the pee button
        {
            if (urineMeter > 0f)
            {
                urineMeter--;

            } else
            {
                urineMeter = 0f; 
            }
            fovChange.resetFOV();
        }
        //play animation of player pissing or something
    }

    public void TooMuchUrine()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if in range to go to bathroom
        if (other.tag == "Bathroom Entrance")
        {
            canEnterBathroom = true; 
        }
        //if in range to pee
        if (other.tag == "Toilet")
        {
            canPee = true;
        }
        //if in range to leave
        if (other.tag == "Bathroom Exit")
        {
            canExitBathroom = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        //if out range to go to bathroom
        if (other.tag == "Bathroom Entrance")
        {
            canEnterBathroom = false;
        }
        //if out range to pee
        if (other.tag == "Toilet")
        {
            canPee = false;
        }
        //if out range to leave
        if (other.tag == "Bathroom Exit") 
        {
            canExitBathroom = false;
        }
    }
}
