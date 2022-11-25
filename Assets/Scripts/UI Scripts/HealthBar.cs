using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(int healthAmount) 
    {
        slider.value = healthAmount;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int healthAmount) 
    {
        slider.maxValue = healthAmount;
        slider.value = healthAmount;
        fill.color = gradient.Evaluate(1f);
    }
}
