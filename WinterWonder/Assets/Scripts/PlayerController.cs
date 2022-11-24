using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
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
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlane == false)
        {
            if (input.axis.magnitude > 0.1f)
            {
                Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
                characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
            }
        }
        if (inPlane == true)
        {
            transform.position = planeSeat.position;
        }
    }
    public void enterPlane() 
    {
        inPlane = true;
        transform.position = planeSeat.position;
    }

   

    


    public void exitPlane()
    {
        inPlane = false;
        transform.position = planeExitPos.position;
    }

}
