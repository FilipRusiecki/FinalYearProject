using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

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



    [Header("Health,stamina,Hunger,Thirst variables")]
    public int maxHealth = 100;
    public int currentHealth;

    public float hunger = 100;
    public float maxHunger = 100;

    public float water = 100;
    public float maxWater = 100;


    public float stamina = 100;
    public float maxStamina = 100;

    public float staminaIncrease = 3;
    public float staminaDrain = 3;



    [Header("HUD Icon bars")]
    public HealthBar healthbar;

    public HungerBar hungerBarScript;
    public Slider hungerBarSlider;

    public WaterBar waterBarScript;
    public Slider waterBarSlider;

    public StaminaBar staminaBarScript;
    public Slider staminaBarSlider;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);

        stamina = maxStamina;
        staminaBarScript.setMaxStamina(maxStamina);

        water = maxWater;
        waterBarScript.setMaxWater(maxWater);

        hunger = maxHunger;
        hungerBarScript.setMaxHunger(maxHunger);
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

            if (Input.GetKeyDown(KeyCode.H))
            {
                currentHealth -= 1;
                healthbar.setHealth(currentHealth);
            }


        }
        if (inPlane == true)
        {
            transform.position = planeSeat.position;
        }

        hungerDrain();
        waterDrain();


    }

    public void enterPlane() 
    {
        inPlane = true;
        //transform.position = planeSeat.position;
    }

    public void exitPlane()
    {
        inPlane = false;
        transform.position = planeExitPos.position;
    }

    public void hungerDrain()
    {

        hunger -=  0.04f * Time.deltaTime;
        hungerBarScript.setHunger(hunger);
    }

    public void waterDrain()
    {

        water -= 0.06f * Time.deltaTime;
        waterBarScript.setWater(water);
    }

}
