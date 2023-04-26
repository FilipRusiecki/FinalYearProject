using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyController : MonoBehaviour
{
    public SkellyTriggerCollider skellyCollider;
    public GameObject playerObject;
    public Rigidbody rb;

    public float MoveSpeed;
    public float attackDistance;
    public float MinDist;

    public void Start()
    {
         rb = GetComponent<Rigidbody>();
         playerObject = GameObject.FindGameObjectWithTag("PlayerVR");
    }

    public void Update()
    {

        playerObject = GameObject.FindGameObjectWithTag("PlayerVR");
        if (skellyCollider.pathFindOn == true)
        {
            moveToPlayer();
        }
        else {
            doNotMoveToPLayer();
        }
        //Debug.Log(Vector3.Distance(transform.position, playerObject.transform.position) + " min distance");
    }


    public void moveToPlayer() {
       
        transform.LookAt(playerObject.transform);

        if (Vector3.Distance(transform.position, playerObject.transform.position) >= MinDist)
        {

         //   Debug.Log("MOVING SKELLY!");

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, playerObject.transform.position) <= attackDistance)
            {
                //Here Call any function U want Like Shoot at here or something
               // Debug.Log("fight");

            }

        }
    
    }
        
   

    public void doNotMoveToPLayer() { 
        //Debug.Log("NOT MOVING SKELLY!");

    }

}
