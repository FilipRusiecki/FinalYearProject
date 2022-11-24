using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    public BearTriggerCollider bearcollider;
    public GameObject playerObject;
    public Rigidbody rb;

    public float MoveSpeed;
    public float attackDistance;
    public float MinDist;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
   
    }

    public void Update()
    {


        if (bearcollider.pathFindOn == true)
        {
            moveToPlayer();
        }
        else
        {
            doNotMoveToPLayer();
        }
        
    }


    public void moveToPlayer()
    {

        transform.LookAt(playerObject.transform);

        if (Vector3.Distance(transform.position, playerObject.transform.position) >= MinDist)
        {

        //    Debug.Log("MOVING BEAR!");

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, playerObject.transform.position) <= attackDistance)
            {
                //Here Call any function U want Like Shoot at here or something
               // Debug.Log("fight");

            }

        }

    }



    public void doNotMoveToPLayer()
    {
        Debug.Log("NOT MOVING BEAR!");

    }

}
