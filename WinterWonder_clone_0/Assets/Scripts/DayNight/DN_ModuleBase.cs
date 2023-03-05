using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DN_ModuleBase : MonoBehaviour
{
    protected DayNightCycle dayNightControl;


    private void OnEnable()
    {
        dayNightControl = this.GetComponent<DayNightCycle>();
        if (dayNightControl != null)
        {
            dayNightControl.AddModule(this);
        }
    }

    private void OnDisable()
    {

        if (dayNightControl != null)
        {
            dayNightControl.removeModule(this); 
        }
    }

    public abstract void UpdateModule(float intensity);
}
