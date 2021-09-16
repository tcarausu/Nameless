using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StaminaScript : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public TMP_Text textMesh;

    public float stamina;   
    public bool hasAttacked { get; set; } = false;

    void Start()
    {
        setMaxStamina(stamina);
    }

    void Update()
    {
    }

    public void setMaxStamina(float staminaValue)
    {
        slider.maxValue = staminaValue;
        slider.value = staminaValue;
    }

    public void setStamina(float staminaValue)
    {
        slider.value = staminaValue;
        stamina = slider.value;
    }

}
