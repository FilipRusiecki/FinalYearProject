using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Mirror;

using Valve.VR.InteractionSystem;

public class PlayerController : NetworkBehaviour
{
    [Header("Player sync")]
    [SyncVar]
    public Vector3 syncPosition;
    [SyncVar]
    public Quaternion syncRotation;

    public Rigidbody rb;


    [Header("PlayerControlls")]
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController characterController;



    [Header("PlaneControlls")]

    public bool inPlane = false;
    public Transform planeSeat;
    public Transform planeExitPos;


    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        characterController = GetComponent<CharacterController>();

        if (!isLocalPlayer)
        {
            GetComponent<CharacterController>().enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (inPlane == false)
        {
            if (input.axis.magnitude > 0.1f)
            {
                Vector3 direction = PlayerVRRR.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
                characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
            }
        }
        if (inPlane == true)
        {
            transform.position = planeSeat.position;
        }
        TransmitPosition();
    }
    public void enterPlane() 
    {
        inPlane = true;
        //transform.position = planeSeat.position;
    }



    [ClientCallback]
    void TransmitPosition()
    {
        CmdProvidePositionToServer(characterController.transform.position);
    }

    /// <summary>
    /// Syncs the position for the server
    /// </summary>
    /// <param name="pos"></param>
    [Command]
    void CmdProvidePositionToServer(Vector3 pos)
    {
        syncPosition = pos;

    }


    public void exitPlane()
    {
        inPlane = false;
        transform.position = planeExitPos.position;
    }

}
