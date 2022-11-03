using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform firepoint;
    public float shootingSpeed = 1;
    public SteamVR_Action_Boolean fireAction;

    public Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if grabbed
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (fireAction[source].stateDown)
            {
                Fire();
            }
        }

    }

    void Fire() {
        Debug.Log("Fire");
        Rigidbody bulletRb = Instantiate(bullet, firepoint.position, firepoint.rotation).GetComponent<Rigidbody>();
        bulletRb.velocity = firepoint.forward * shootingSpeed;
       
    }
}
