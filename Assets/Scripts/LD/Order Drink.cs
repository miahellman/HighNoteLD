using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.HID;

public class OrderDrink : MonoBehaviour
{
    [Header("Order System")]
    public bool canOrder;
    public DialogueTrigger dialogueTrigger;
    [SerializeField] GameObject normalDrink, stoolDrink, tableDrink, wallDrink, boothDrink, personDrink;
    [SerializeField] Transform spawnPosition; 
    public bool normalOrder, stoolOrder, tableOrder, wallOrder, boothOrder, personOrder; //add the rest later and create system to switch between them;  

    // Start is called before the first frame update
    void Start()
    {
        normalOrder = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (canOrder && Input.GetKeyDown(KeyCode.E))
        {
            OrderingSystem(); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Order")
        {
            canOrder = true;
            dialogueTrigger.TriggerDialogue();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Order")
        {
            canOrder = false;
        }
    }
    private void OrderingSystem()
    {
        if (normalOrder)
        {
            Instantiate(normalDrink, spawnPosition.position, spawnPosition.rotation);
        }
        else if (stoolOrder)
        {
            Instantiate(stoolDrink, spawnPosition.position, spawnPosition.rotation);
        }
        else if (tableOrder)
        {
            Instantiate(tableDrink, spawnPosition.position, spawnPosition.rotation);
        }
        else if (boothOrder)
        {
            Instantiate(boothDrink, spawnPosition.position, spawnPosition.rotation);
        }
        else if (wallOrder)
        {
            Instantiate(wallDrink, spawnPosition.position, spawnPosition.rotation);
        }
        else if (personOrder)
        {
            Instantiate(personDrink, spawnPosition.position, spawnPosition.rotation);
        }
    }
}
