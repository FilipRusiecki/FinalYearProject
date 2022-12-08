using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyTriggerCollider : MonoBehaviour
{
    public SkellyController skellyController;
    public bool pathFindOn;
    private void Start()
    {
        pathFindOn = false;
       // skellyController = GetComponent<SkellyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pathFindOn = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pathFindOn = false;
        }
    }
}
