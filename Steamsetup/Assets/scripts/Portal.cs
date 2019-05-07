using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject otherPortal;

    public bool portalOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && otherPortal.GetComponent<Portal>().portalOn)
        {
            Rigidbody otherRigidBody = other.GetComponent<Rigidbody>();

            Vector3 exitVelocity = otherPortal.transform.forward * otherRigidBody.velocity.magnitude;
            otherRigidBody.velocity = exitVelocity;

            other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 2;

        }
    }
    
}
