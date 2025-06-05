using UnityEngine;
using UnityEngine.UI;

public class HealthViewBar : HealthView
{
    [SerializeField] private Slider _slider;

    protected override void OnValueChanged(float currentHealth, float maxHealth)
    {
        _slider.value = currentHealth / maxHealth; 
    }
}
