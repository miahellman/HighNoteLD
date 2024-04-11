using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingBodies : MonoBehaviour
{
    [Header("Player Data")]
    public PlayerMovement pm;
    public float switchSpeedMax;
    public float switchSpeedModifier;

    [Header("Target Data")]
    public GameObject targetGameObject;
    public string targetName;

    [Header("Camera Data")]
    [SerializeField] Transform cameraTransform;
    public float shakeFrequency;

    Vector3 originalCamPos;

    bool targetAchieved;
    float switchSpeed = 0; 
    //public GameObject 
    
    // Start is called before the first frame update
    void Start()
    {
        originalCamPos = cameraTransform.position; //assign current cam pos
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void FixedUpdate()
    {
        if (pm.switchingBody)
        {
            Vector3 pBody = transform.position;
            pm.visual.transform.parent = null;
            targetGameObject = GameObject.Find(targetName);

            Vector3 targetPosition = targetGameObject.transform.position;
            if (pBody != targetPosition)
            {
                if (switchSpeed < switchSpeedMax) { switchSpeed += switchSpeedModifier;  }
                Vector3 newPos = Vector3.MoveTowards(pBody, targetPosition, switchSpeed);
                pBody = newPos;
            }
            transform.position = pBody;
            //new Vector3(pBody.x, transform.position.y, pBody.z); 
            if (pBody == targetPosition)
            {
                targetAchieved = true;
            }

            //CameraShake(); Didn't end up having the time to properly implement Camera Shake

            if (targetAchieved)
            {
                pm.visual = targetGameObject;
                targetGameObject.transform.parent = transform;

                Vector3 finalBump = transform.position;
                finalBump.y += 2; 
                transform.position = finalBump;

                // transform.position = new Vector3(transform.position.x, transform.position.y + landModifier, transform.position.z);
                switchSpeed = 0;
                //StopShake();
                targetAchieved = false;
                pm.switchingBody = false;
            }
        }
    }

    public void CameraShake()
    {
        //moves camera to random point chosen within circle around the camera
        cameraTransform.position = originalCamPos + Random.insideUnitSphere * shakeFrequency;
    }

    public void StopShake()
    {
        //return camerea to original position
        cameraTransform.position = originalCamPos;
    }
}







