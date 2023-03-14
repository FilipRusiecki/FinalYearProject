using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ColliderFollowController : NetworkBehaviour
{
    [Header("Player sync")]
    [SyncVar]
    public Vector3 syncPosition;
    [SyncVar]
    public Quaternion syncRotation;

    private CharacterController charController;
    public Transform centerEye;

    private void Start()
    {
        charController = GetComponent<CharacterController>();

        if (!isLocalPlayer)
        {
            GetComponent<CharacterController>().enabled = false;
            return;
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        TransmitPosition();
    }


    [ClientCallback]
    void TransmitPosition()
    {
        CmdProvidePositionToServer(charController.transform.position);
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

    private void LateUpdate()
    {
        Vector3 newCenter = transform.InverseTransformVector(centerEye.position - transform.position);
        charController.center = new Vector3(newCenter.x, charController.center.y, newCenter.z);
    }
}