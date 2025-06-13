using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewSmoothBar : HealthView
{
    [SerializeField] private Slider _smoothSlider;
    [SerializeField][Range(0.1f, 1f)] private float _smoothSpeed;

    private Coroutine _smoothCoroutine;

    protected override void Awake()
    {
        base.Awake();
        _smoothSlider.value = _health.CurrentHealth / _health.MaxHealth;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (_smoothCoroutine != null)
        {
            StopCoroutine(_smoothCoroutine);
        }
    }

    protected override void OnValueChanged(float currentHealth, float maxHealth)
    {
        if (_smoothCoroutine != null)
        {
            StopCoroutine(_smoothCoroutine);
        }

        _smoothCoroutine = StartCoroutine(SmoothHealthCoroutine(currentHealth / maxHealth));
    }

    private IEnumerator SmoothHealthCoroutine(float currentNormalized)
    {
        while (Mathf.Abs(_smoothSlider.value - currentNormalized) > 0.001f)
        {
            _smoothSlider.value = Mathf.MoveTowards(_smoothSlider.value, currentNormalized, _smoothSpeed * Time.deltaTime);

            yield return null;
        }

        _smoothCoroutine = null;
    }
}
