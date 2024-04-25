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
    [SerializeField] float drunkLimit, stoolLimit, tableLimit, boothLimit, wallLimit, personLimit;
    [SerializeField] bool canDrink; 

    public int drinksDrunk;

    [Header("External Modifiers")]
    //public Grab grab;
    public UrineManager urineManager; 
    public GameObject drinkObject;
    public FOVChange fovChange;
    public OrderDrink orderDrink;

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
        else { canDrink = false;}
        DrinkableSystem();
    }

    public void DrinkableSystem()
    {
       // Debug.Log("I'M WORKING ");
        if (drinksDrunk >= stoolLimit)
        {
            orderDrink.normalOrder = false; orderDrink.stoolOrder = true;
        }
        if (drinksDrunk >= tableLimit)
        {
            orderDrink.stoolOrder = false; orderDrink.tableOrder = true;
        }
        if (drinksDrunk >= boothLimit)
        {
            orderDrink.tableOrder = false; orderDrink.boothOrder = true;
        }
        if (drinksDrunk >= wallLimit)
        {
            orderDrink.boothOrder = false; orderDrink.wallOrder = true;
        }
        if (drinksDrunk >= personLimit)
        {
            orderDrink.wallOrder = false; orderDrink.personOrder = true;
        }
    }
}
