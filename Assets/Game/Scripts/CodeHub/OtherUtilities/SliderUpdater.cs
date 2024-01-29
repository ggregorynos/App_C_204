using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private float _value;
    private float _maxValue;
    
    public Action<float> OnValueChanged;
    public Action<float> OnMaxValueChanged;
    
    public void Initialize(float startValue, float maxValue)
    {
        _value = startValue;
        _maxValue = maxValue;
    }

    public void UpdateValue(float value)
    {
        _slider.value = value;
        OnValueChanged?.Invoke(value);
    }
    
    public void UpdateMaxValue(float value)
    {
        _slider.maxValue = value;
        OnMaxValueChanged?.Invoke(value);
    }
    
    public void UpdateMaxValue(int value)
    {
        UpdateMaxValue((float)value);
    }
    
    public void UpdateValue(int value)
    {
        UpdateValue((float)value);
    }
}
