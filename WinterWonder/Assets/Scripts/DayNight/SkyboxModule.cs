using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxModule : DN_ModuleBase
{
    [SerializeField]
    private Gradient skyColour;
    [SerializeField]
    private Gradient horizonColour;
    public override void UpdateModule(float intensity)
    {
        RenderSettings.skybox.SetColor("_SkyTint", skyColour.Evaluate(intensity));
        RenderSettings.skybox.SetColor("_GroundColor", horizonColour.Evaluate(intensity));

    }
}
