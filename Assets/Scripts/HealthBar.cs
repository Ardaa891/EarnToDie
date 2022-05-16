using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Current;
    
    public Slider slider;

    private void Awake()
    {
        Current = this;
    }

    private void Start()
    {
        
        
    }


   
 





    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

    }
    
    
    
    
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void UpdateHealth(float value)
    {
        CarController.Current.maxHealth += value;
        CarController.Current.currentHealth += value;
        PlayerPrefs.SetFloat("NewMaxHealth", CarController.Current.maxHealth);
        PlayerPrefs.SetFloat("NewCurrentHealth", CarController.Current.currentHealth);
    }
}
