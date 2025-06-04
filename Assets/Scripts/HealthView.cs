using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Health))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _smoothSlider;
    [SerializeField] [Range(0.1f, 1f)] private float _smoothSpeed;

    private Health _health;
    private Coroutine _smoothCoroutine;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnValueChanged;

        if (_smoothCoroutine != null)
        {
            StopCoroutine(_smoothCoroutine);
        }
    }

    private void OnValueChanged(float currentHealth, float maxHealth)
    {
        _text.text = $"{currentHealth}/{maxHealth}";

        float normalizedHealth = currentHealth / maxHealth;
        _slider.value = normalizedHealth;

        if (_smoothCoroutine != null)
        {
            StopCoroutine(_smoothCoroutine);
        }

        _smoothCoroutine = StartCoroutine(SmoothHealthCoroutine(normalizedHealth));
    }

    private IEnumerator SmoothHealthCoroutine(float currentNormalized)
    {
        while (Mathf.Abs(_smoothSlider.value - currentNormalized) > 0.001f)
        {
            _smoothSlider.value = Mathf.MoveTowards(_smoothSlider.value, currentNormalized, _smoothSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
