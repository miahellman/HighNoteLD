using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; //the prefab that we will be instantiating each time we launch a projectile
    PlayerInput playerInput;
    public PlayerMovement pm;
    bool projectileTriggered; //set to true for the frame when the player triggers the projectile action
    Transform cameraHolder; //gives us access to the cameraHolder so we can know its position and rotation

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cameraHolder = transform.Find("CameraHolder"); //finds a child of the player object called CameraHolder
    }


    // Update is called once per frame
    void Update()
    {
        if (pm.switchingBody)
        {
            projectileTriggered = playerInput.actions["Projectile"].triggered; //check if the player has triggered the projectile action - RIGHT MOUSE BUTTON

            if (projectileTriggered)
            { //if the player has triggered the projectile action, instantiate a projectile
              //instantiates a projectile at the position of the camera holder PLUS 4 * the forward direction of the camera holder so that the projectile starts a bit ahead of the player and not inside the player's head
              //the rotation of the projectile is the same as the rotation of the camera holder so that it is launched in the direction the player is looking
                Instantiate(projectilePrefab, cameraHolder.transform.position + cameraHolder.transform.forward * 4, cameraHolder.transform.rotation);
            }
        }
    }
}
