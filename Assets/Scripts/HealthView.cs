using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class HealthView : MonoBehaviour
{
    protected Health _health;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
    }

    protected virtual void OnEnable()
    {
        _health.HealthChanged += OnValueChanged;
    }

    protected virtual void OnDisable()
    {
        _health.HealthChanged -= OnValueChanged;
    }

    protected abstract void OnValueChanged(float currentHealth, float maxHealth);
}
