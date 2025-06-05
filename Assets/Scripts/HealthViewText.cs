using UnityEngine;
using TMPro;

public class HealthViewText : HealthView 
{
    [SerializeField] private TextMeshProUGUI _text;

    protected override void OnValueChanged(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth}/{maxHealth}";
    }
}
