using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Drink : MonoBehaviour
{
    [Header("Internal Constraints")]
    [SerializeField] float drinkSizeModifier;
    [SerializeField] float drinkSizeLimit;
    [SerializeField] float drunkLimit;
    [SerializeField] bool canDrink;

    public int drinksDrunk;

    [Header("External Modifiers")]
    //public Grab grab;
    public UrineManager urineManager; 
    public GameObject drinkObject;
    public FOVChange fovChange; 

    Vector3 drinkSize;
    string drinkInputAxis = "Mouse ScrollWheel";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drinkObject != null)
        {
            drinkSize = drinkObject.transform.localScale;
            if (drinkSize.x > drinkSizeLimit) //jist checking one axis cause all axis' should be the same
            {
                canDrink = true;
            }

            if (canDrink && (Input.GetAxisRaw(drinkInputAxis)) != 0)
            //CHASE NOTES:
            //there is the infastructure to increase/decrease beer size based on scroll direction as this axis outputs .1 (UP), 0(NULL), -.1(DOWN)
            //Maybe scrolling up makes you vom
            {
                drinkSize = new Vector3(drinkSize.x - drinkSizeModifier, drinkSize.y - drinkSizeModifier, drinkSize.z - drinkSizeModifier);
                drinkObject.transform.localScale = drinkSize;

                urineManager.urineMeter++; //every time you take a sip you need to pee a bit more

                //Debug.Log("drinking");
                if (drinksDrunk >= drunkLimit) //if the player is drunk
                {
                    fovChange.increaseFOV();
                }
            }

            if (drinkSize.x <= drinkSizeLimit)
            {
                drinksDrunk++;
             
                
                Destroy(drinkObject);
                drinkObject = null;
            }

        }
        else { canDrink = false;  }
    }
}
