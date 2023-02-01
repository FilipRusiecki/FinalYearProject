using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonModule : DN_ModuleBase
{


    [SerializeField]
    private Light moon;
    [SerializeField]
    private Gradient moonColour;
    [SerializeField]
    private float baseIntensity;
    public override void UpdateModule(float intensity)
    {
        moon.color = moonColour.Evaluate(1 - intensity);
        moon.intensity = (1 - intensity) * baseIntensity + 0.05f;
    }
}
