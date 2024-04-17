using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("setup")]
    public bool switchingBody;
    public GameObject visual;
    public GameManager gameManager; 
    [SerializeField] float speed; //how fast the player moves using velocityInput
    [SerializeField] float gravity, jumpHeight; //set these values to get the gravity and jump height you want

    CharacterController characterController; //gives us access to the character controller
    PlayerInput playerInput; //gives us access to the player input
    Vector3 velocity, velocityInput, velocityGravity, velocitySpecial; //our velocity is split into three separate velocities: velocityInput comes from the player input, velocityGravity is the jumping/gravity velocity, and velocitySpecial is for any special actions that affect the movement (like enemy shoves or gravity pulls)
 
    float axisHorizontal, axisForward; //stores the horizontal and forward inputs (which we will get from WASD or the left analog stick)
    float jumpVelocity; //this value should not be set in the inspector because we are using an equation below to calculate jumpVelocity based on jumpHeight and gravity
    bool jumpTriggered;



    void Start()
    {
        //get access to the character controller and player input components
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void Update()
    {

        if (gameManager.readyWASD)
        {

            if (visual.name == "Disk") //sometimes this works sometimes it doesn't idk why
            {
                gravity = 45f;
                jumpHeight = 2f;
                speed = 3f;
                jumpVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
            else if (visual.name == "Cube")
            {
                gravity = 25f;
                jumpHeight = 3f;
                speed = 4f;
                jumpVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
            else if (visual.name == "Sphere")
            {
                gravity = 15f;
                jumpHeight = 8f;
                speed = 6f;
                jumpVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
            else
            {
                gravity = 30f;
                jumpHeight = 4f;
                speed = 5f;
                jumpVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity); //calculate the jump velocity using the jump height and gravity. You can set the jump height in meters and get the jump velocity that would lead you to jump that high with the curent gravity. This only works for realistic up/down movement (e.g. if you try to make a player go down faster than it goes up, this wouldn't work)
            }
            //get the horizontal and forward inputs as well as the jumpTriggered value from the playerInput using Unity's new-ish Input System
            axisHorizontal = playerInput.actions["Move"].ReadValue<Vector2>().x;
            axisForward = playerInput.actions["Move"].ReadValue<Vector2>().y;
            jumpTriggered = playerInput.actions["Jump"].triggered;

            //if you just pressed the jump button this frame and the character is on the ground, being the jump
            if (jumpTriggered && characterController.isGrounded)
            {
                Jump();
            }
        }

    }

    void FixedUpdate()
    {
        if (gameManager.readyWASD)
        {

            Vector3 forwardInput = transform.forward * axisForward; //set how the player should be moving forward by multiplying the forward vector (the direction the player is currently facing) by the forward input (from the W/S keys on the ekyeboard or up/down on the left analog stick)
            Vector3 horizontalInput = transform.right * axisHorizontal; //set how the player should be moving horizontally by multiplying the right vector (the direction to the right of the player) by the horizontal input (from the A/D keys on the keyboard or left/right on the left analog stick)

            velocityInput = forwardInput + horizontalInput; //add the forward and horizontal inputs together to get the total velocity input
            velocityInput.Normalize(); //normalize the velocity input (if the Vector is no zero, make sure the vector length is 1) so that the player doesn't move faster when moving diagonally
            velocityInput *= speed; //multiply the velocityInput by speed so that the player moves at the speed we set. This is thesame as velocityInput = velocityInput * speed;

            velocityGravity.y -= gravity * Time.fixedDeltaTime; //pull the player's velocityGravity.y down by gravity. Time.fixedDeltaTime is the time between each FixedUpdate call - using it allows us to set the gravity in meters per second instead of meters per frame.

            //if the character is on the ground, don't let gravity pull its velocityGravity.y to a really low number. But do keep it slightly below 0 so that the player move more nicely down a slope.
            if (characterController.isGrounded && velocityGravity.y < 0)
            {
                velocityGravity.y = -.1f;
            }

            velocity = velocityInput + velocityGravity + velocitySpecial; //add the three separate velocities into one final velocity. this will be useful once we use the other velocities.
            characterController.Move(velocity * Time.fixedDeltaTime); //move the character controller using the final velocity. Time.fixedDeltaTime is the time between each FixedUpdate call - using it allows us to set the speed of the player in meters per second instead of meters per frame. }

        }
    }
/// <summary>
/// Begin the jump by setting the velocityGravity.y to the jump velocity
/// </summary>
    void Jump()
        {
            if (gameManager.readyWASD) { velocityGravity.y = jumpVelocity; }
        }
        
    

}
