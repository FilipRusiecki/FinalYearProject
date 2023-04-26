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
    public int magazineSize;
    public int availableAmmo;
    private int ammoTakenToReload;
    public int bulletsPerTap;
    public int bulletsLeft;
    int bulletsShot;
    public float timeBetweenShots;
    public float timeBetweenShooting;
    public float reloadTime;
    public bool allowButtonHold;
    public bool shooting;
    public bool readyToShoot;
    public bool reloading;
    public Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        interactable = GetComponent<Interactable>();
    }
    // Update is called once per frame
    void Update()
    {
        //check if grabbed
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (allowButtonHold) shooting = fireAction[source].state;
            else shooting = fireAction[source].stateDown;

            if (readyToShoot && shooting && bulletsLeft > 0) {
                bulletsShot = bulletsPerTap;
                Fire();
            }
            //if (fireAction[source].stateDown)
            //{
            //    Fire();
            //}
        }

    }
    void Fire() {

        readyToShoot = false;
        Debug.Log("Fire");

  
        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);
        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Fire", timeBetweenShots);
        }
        Rigidbody bulletRb = Instantiate(bullet, firepoint.position, firepoint.rotation).GetComponent<Rigidbody>();
        bulletRb.velocity = firepoint.forward * shootingSpeed;

    }
    private void ResetShot()    //just a reset of the function to be able to shoot 
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        if (magazineSize == 0)
        {
            ammoTakenToReload = bulletsLeft - magazineSize;
            availableAmmo += ammoTakenToReload;
            reloading = true;
            Invoke("ReloadFinished", reloadTime);
        }
        
    }

    private void ReloadFinished()
    {
        bulletsLeft -= ammoTakenToReload;
        reloading = false;
    }
}
