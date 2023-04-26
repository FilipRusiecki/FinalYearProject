using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTriggerCollider : MonoBehaviour
{

    public BearController bearcontroller;
    public bool pathFindOn;
    private void Start()
    {
        pathFindOn = false;
   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerVR"))
        {
            pathFindOn = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerVR"))
        {
            pathFindOn = false;
        }
    }
}

