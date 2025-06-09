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
        _health.ValueChanged += OnValueChanged;
    }

    protected virtual void OnDisable()
    {
        _health.ValueChanged -= OnValueChanged;
    }

    protected abstract void OnValueChanged(float currentHealth, float maxHealth);
}
