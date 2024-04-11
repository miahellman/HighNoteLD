using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] float speed = 20f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
    }

    void FixedUpdate(){
        //move the projectile forward by speed. Forward is the direction the projectile is currently facing.
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }
}
