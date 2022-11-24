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




    public bool turnLeft = false;
    public bool turnRight = false;
    public bool dive = false;
    public bool climb = false;


    public float speed = 1.0f;

    public float turnSpeed = 0.4f;



  //  public Interactable interactable;


    public PlayerController player;


    private void Start()
    {
       // interactable = GetComponent<Interactable>();
        
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
        }
    }


    private void ForwardMovement() 
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        //speed -= transform.forward.y * Time.deltaTime * 50.0f;
         Debug.Log("plane moving");
        if (speed < 10)
        {
            speed = 10;
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
            transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
        }
        if (eulerAngZ > 90.0f && eulerAngZ < 92.0f)
        {
            eulerAngX = eulerAngX + 0.2f;
            eulerAngZ = 90.0f;
            transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
        }
    }

    private void Turn()
    {
        if (turnLeft)
        {
            eulerAngZ = eulerAngZ + turnSpeed;
            transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
        }
        if (turnRight)
        {
            eulerAngZ = eulerAngZ - turnSpeed;
            transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
        }
        if (dive)
        {
            eulerAngX = eulerAngX + turnSpeed;
            transform.eulerAngles = new Vector3(eulerAngX, eulerAngY, eulerAngZ);
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

        }

    }



    private void GetTurn()
    {
        ////if (ORVInput.GetDown(ORVInput.Button.PrimaryThumbStickLeft, ORVInput.Controller.RTouch))
        ////{
        ////    turnLeft = true;
        ////}
        ////if (ORVInput.GetUp(ORVInput.Button.PrimaryThumbStickLeft, ORVInput.Controller.RTouch))
        ////{
        ////    turnLeft = false;
        ////}

        ////if (ORVInput.GetDown(ORVInput.Button.PrimaryThumbStickRight, ORVInput.Controller.RTouch))
        ////{
        ////    turnRight = true;
        ////}
        ////if (ORVInput.GetUp(ORVInput.Button.PrimaryThumbStickRight, ORVInput.Controller.RTouch))
        ////{
        ////    turnRight = false;
        ////}




        ////if (ORVInput.GetDown(ORVInput.Button.PrimaryThumbStickRight, ORVInput.Controller.RTouch))
        ////{
        ////    dive = true;
        ////}
        ////if (ORVInput.GetUp(ORVInput.Button.PrimaryThumbStickRight, ORVInput.Controller.RTouch))
        ////{
        ////    dive = true;
        ////}


        ////if (ORVInput.GetDown(ORVInput.Button.PrimaryThumbStickRight, ORVInput.Controller.RTouch))
        ////{
        ////    climb = true;
        ////}
        ////if (ORVInput.GetUp(ORVInput.Button.PrimaryThumbStickRight, ORVInput.Controller.RTouch))
        ////{
        ////    climb = true;
        ////}

    }

}
