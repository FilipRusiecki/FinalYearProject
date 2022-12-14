using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlanePilot : MonoBehaviour
{

    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;

    public Rigidbody rigidbody;
   


    public bool turnLeft = false;
    public bool turnRight = false;
    public bool dive = false;
    public bool climb = false;


    public float speed;

    public float turnSpeed;



  //  public Interactable interactable;


    public PlayerController player;

    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean PlaneAction;


    private void Start()
    {
        // interactable = GetComponent<Interactable>();
        rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {


        if (player.inPlane == true)
        {
            GetAngles();
            GetTurn();
            Turn();
            Stablize();
            ForwardMovement();
            CheckCollisionWithTerrain();
        }
    }

    private void CheckCollisionWithTerrain() 
    {
        float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);



        
        if (terrainHeightWhereWeAre >= transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, terrainHeightWhereWeAre, transform.position.z);
           // rigidbody.MovePosition = new Vector3(transform.position.x, terrainHeightWhereWeAre, transform.position.z);
        }
    }

        
    private void ForwardMovement() 
    {
 
        rigidbody.AddForce(this.gameObject.transform.forward * Time.deltaTime* speed);  // <<<<<------------------------------ use adding force as chaning the transform makes a stuttering effect on the physics and MAKE SURE that the rigid body is not kinematic or physics wont work
        //rigidbody.AddForce( transform.forward * speed,ForceMode.Acceleration);

        //speed -= transform.forward.y * Time.deltaTime * 50.0f;
        Debug.Log("plane moving");
        if (speed < 10)
        {
            speed = 1;
        }
    }

    private void GetAngles()
    {
        eulerAngX = transform.localEulerAngles.x;
        eulerAngY = transform.localEulerAngles.y;
        eulerAngZ = transform.localEulerAngles.z;
    }

    private void Stablize()
    {
        if (eulerAngZ < 270.0f && eulerAngZ > 268.0f)
        {
            eulerAngX = eulerAngX + 0.2f;
            eulerAngZ = 270.0f;
            rigidbody.transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
            //rigidbody.AddForce(transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ),ForceMode.Impulse);

        }
        if (eulerAngZ > 90.0f && eulerAngZ < 92.0f)
        {
            eulerAngX = eulerAngX + 0.2f;
            eulerAngZ = 90.0f;
            rigidbody.transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
            //rigidbody.AddForce(transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ), ForceMode.Impulse);

        }
    }

    private void Turn()
    {
        if (turnLeft)
        {
            eulerAngZ = eulerAngZ + turnSpeed;
           transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
           // rigidbody.AddForce(transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ), ForceMode.Impulse);

        }
        if (turnRight)
        {
            eulerAngZ = eulerAngZ - turnSpeed;
            transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
           //rigidbody.AddForce(transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ), ForceMode.Impulse);

        }
        if (dive)
        {
            eulerAngX = eulerAngX + turnSpeed;
            transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
            //rigidbody.AddForce(transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ), ForceMode.Impulse);

        }
        if (climb)
        {
            bool sharpTurnNoLiftLeft = eulerAngZ < 92 && eulerAngZ > 69;
            bool sharpTurnLittleLiftLeft = eulerAngZ < 69 && eulerAngZ > 49;
            bool mildTurnAndLiftLeft = eulerAngZ < 49 && eulerAngZ > 29;

            bool sharpTurnNoLiftRight = eulerAngZ > 269 && eulerAngZ < 292;
            bool sharpTurnLittleLiftRight = eulerAngZ > 292 && eulerAngZ < 312;
            bool mildTurnAndLiftRight = eulerAngZ > 312 && eulerAngZ < 332;
            if (sharpTurnNoLiftLeft)
            {
                eulerAngY -= turnSpeed * Time.deltaTime * 100.0f;
            }
            else if (sharpTurnLittleLiftLeft)
            {
                eulerAngY -= turnSpeed * Time.deltaTime * 85.0f;

                eulerAngX -= turnSpeed * Time.deltaTime * 20.0f;
            }
            else if (mildTurnAndLiftLeft)
            {
                eulerAngY -= turnSpeed * Time.deltaTime * 40.0f;

                eulerAngX -= turnSpeed * Time.deltaTime * 60.0f;

            }
            else if (sharpTurnNoLiftRight)
            {
                eulerAngY += turnSpeed * Time.deltaTime * 100.0f;
            }
            else if (sharpTurnLittleLiftRight)
            {
                eulerAngY += turnSpeed * Time.deltaTime * 85.0f;

                eulerAngX -= turnSpeed * Time.deltaTime * 20.0f;
            }
            else if (mildTurnAndLiftRight)
            {
                eulerAngY += turnSpeed * Time.deltaTime * 40.0f;

                eulerAngX -= turnSpeed * Time.deltaTime * 60.0f;

            }
            else {
                eulerAngX -= turnSpeed;
            }
           
                transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
            //rigidbody.AddForce(transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ),ForceMode.Impulse);
            //player.transform.position = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
        }

    }



    private void GetTurn()
    {
        if (input.axis.x < -0.75f && input.axis.y < 0.3f && PlaneAction.stateDown)
        {
                turnLeft = true;
        }

         if (input.axis.x < -0.75f && input.axis.y < 0.3f && PlaneAction.stateUp)
         {
                turnLeft = false;
         }



         if (input.axis.x > 0.6f && input.axis.y < 0.3f && PlaneAction.stateDown)
         {
                turnRight = true;
         }
         if (input.axis.x > 0.6f && input.axis.y < 0.3f && PlaneAction.stateUp)
         {
                turnRight = false;
         }





            if (input.axis.x > -0.45f && input.axis.y < -0.5f && PlaneAction.stateDown)
        {
            
            dive = true;
        }
        if (input.axis.x > -0.45f && input.axis.y < -0.5f && PlaneAction.stateUp)
        {
       
            dive = false;
        }




        if (input.axis.x > -0.45f && input.axis.y > 0.5f && PlaneAction.stateDown )
        {
          
            climb = true;
        }
        if (input.axis.x > -0.45f && input.axis.y > 0.5f && PlaneAction.stateUp)
        {
          
            climb = false;
        }
        Debug.Log("x: " + input.axis.x + " y: " + input.axis.y);


    }

}
