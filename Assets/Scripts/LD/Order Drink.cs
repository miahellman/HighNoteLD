using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class OrderDrink : MonoBehaviour
{
    [Header("Order System")]
    public bool canOrder;
    [SerializeField] GameObject normalDrink, stoolDrink, tableDrink;
    [SerializeField] Transform spawnPosition; 
    [SerializeField] bool normalOrder, stoolOrder, tableOrder; //add the rest later and create system to switch between them;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOrder && Input.GetKeyDown(KeyCode.E))
        {
            if (normalOrder)
            {
                Instantiate(normalDrink, spawnPosition.position, spawnPosition.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Order")
        {
            canOrder = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Order")
        {
            canOrder = false;
        }
    }
}
