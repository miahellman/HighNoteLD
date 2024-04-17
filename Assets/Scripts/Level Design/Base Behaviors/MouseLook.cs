using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public PlayerMovement pm; 
    PlayerInput playerInput; //gives us access to the player input
    float inputHorizontal, inputVertical, xRotation; //inputHorizontal and inputVertical store the change (delta) in mouse position this frame. xRotation stores the current x rotation (up/down rotation) of the camera.
    [SerializeField] float mouseSensitivity; //how much the camera should rotate when the mouse moves
    [SerializeField] Transform cameraHolder; //the cameraHolder is the parent of the camera. It is used to rotate the camera up and down (x rotation) without rotating the player. It also allows the camera to do funny things (like screen shake) without affecting the cameraHolder's position
    public GameManager gameManager; 
    void Start()
    {
        playerInput = GetComponent<PlayerInput>(); //get access to the player input component
        Cursor.lockState = CursorLockMode.Locked; //disables the mouse cursor
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.readyMouse == true)
        {
            //get the horizontal and vertical inputs from the playerInput using Unity's new-ish Input System
            inputHorizontal = playerInput.actions["Look"].ReadValue<Vector2>().x;
            inputVertical = playerInput.actions["Look"].ReadValue<Vector2>().y;


            float inputHorizontalThisFrame = inputHorizontal * mouseSensitivity * Time.deltaTime; //multiply the horizontal (X) input by the mouse sensitivity and
                                                                                                  //the time between frames. This will get us how much the camera should
                                                                                                  //rotate left/right (around the Y axis) this frame

            float inputVerticalThisFrame = inputVertical * mouseSensitivity * Time.deltaTime; //multiply the vertical (Y) input by the mouse sensitivity and the time
                                                                                              //between frames. This will get us how much the camera should rotate up/down (around the X axis) this frame

            //x rotation: up/down
            xRotation -= inputVerticalThisFrame; //subtract the vertical input from the x rotation. This is done so that moving the mouse up will rotate the camera up
                                                 //(x rotation decreases) and moving the mouse down will rotate the camera down (x rotation increases)

            xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamp the x rotation between -90 and 90 degrees. This is done so that the player can't rotate the
                                                           //camera more than 90 degrees up or down (which would make the camera flip upside down).
                                                           //This makes it so that the player can't look beyond straight-down or straight-up.

            cameraHolder.localRotation = Quaternion.Euler(xRotation, 0, 0); //rotate the cameraHolder up/down (x rotation) by the x rotation we calculated.
                                                                            //This is done so that the camera rotates up/down around the cameraHolder's local X axis
                                                                            //(which is the same as the camera's local X axis). Quaternion.Euler is used to convert the
                                                                            //x rotation from Euler angles (the 360 degree angles we are most used to) into a Quaternion
                                                                            //that Unity can use to rotate the cameraHolder.

            //y rotation: left/right
            transform.Rotate(Vector3.up * inputHorizontalThisFrame); //rotate the player left/right (around the Y axis) by the horizontal input.
                                                                     //This is done so that moving the mouse left will rotate the player left (y rotation decreases)
                                                                     //and moving the mouse right will rotate the player right (y rotation increases). Vector3.up is
                                                                     //used to get the world up vector (the direction the world is oriented in) and multiply it by the horizontal
                                                                     //input to get the rotation around the world up vector. We rotate the PLAYER here and not the cameraHolder
                                                                     //because we want the player's body facing the corect direction since the player walks in that direction when they hold W.
        }

    }
}
