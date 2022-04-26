using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasBar : MonoBehaviour
{
    public static GasBar Current;

    public Slider slider;

    private void Awake()
    {
        Current = this;
    }

    public void SetMaxGas(float gas)
    {
        slider.maxValue = gas;
        slider.value = gas;
    }

    public void SetGas(float gas)
    {
        slider.value = gas;
    }
}
