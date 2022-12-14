using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setWater(float t_stamina)
    {
        slider.value = t_stamina;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setMaxWater(float t_stamina)
    {
        slider.maxValue = t_stamina;
        slider.value = t_stamina;

        fill.color = gradient.Evaluate(1f);
    }

}
