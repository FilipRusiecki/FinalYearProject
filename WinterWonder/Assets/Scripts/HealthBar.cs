using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setHealth(float t_stamina)
    {
        slider.value = t_stamina;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setMaxHealth(float t_stamina)
    {
        slider.maxValue = t_stamina;
        slider.value = t_stamina;

        fill.color = gradient.Evaluate(1f);
    }

}
