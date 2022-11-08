using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyController : MonoBehaviour
{
    public SkellyTriggerCollider skellyCollider;
    public GameObject playerObject;
    public Rigidbody rb;
    private void Start()
    {
         rb = GetComponent<Rigidbody>();
        //skellyCollider = GetComponent<SkellyTriggerCollider>();
    }

    private void Update()
    {
        if (skellyCollider.pathFindOn == true)
        {
            moveToPlayer();
        }
        else {
            doNotMoveToPLayer();
        }
    }


    public void moveToPlayer() {
        //  rb.transform.position = playerObject.transform.position;
        if (rb.transform.position.x < 100)
        {

            rb.transform.position+= new Vector3(-0.01f, 0.0f, 0.0f);
        }
        Debug.Log("MOVING SKELLY!");
        
    }

    public void doNotMoveToPLayer() { 
        Debug.Log("NOT MOVING SKELLY!");

    }

}
