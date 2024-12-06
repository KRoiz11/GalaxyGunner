using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    // updates the value of the slider 
    public void SetSlider(float amount)
    {
        healthSlider.value = amount;
    }

    //sets the slider to the max value
    public void SetSliderMax(float amount)
    {
        healthSlider.maxValue = amount;
        SetSlider(amount);
    }
}
