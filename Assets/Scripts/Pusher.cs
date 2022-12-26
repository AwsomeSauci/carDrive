using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    private Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            rb = other.GetComponentInParent<Rigidbody>();
            rb.velocity = rb.velocity+transform.forward*rb.velocity.magnitude*0.15f;            
        }
    }
    
}

