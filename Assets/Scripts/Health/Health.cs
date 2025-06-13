using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHitPoints;
    [SerializeField] private float _hitPoints;

    public event Action<float, float> ValueChanged;

    public float MaxHealth => _maxHitPoints;
    public float CurrentHealth => _hitPoints;

    public void ApplyDamage(float damage)
    {
        if (IsPositiveValue(damage))
        {
            _hitPoints -= damage;
            ValueChanged?.Invoke(_hitPoints, _maxHitPoints);

            if (_hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ApplyHeal(float heal)
    {
        if (IsPositiveValue(heal))
        {
            _hitPoints = Mathf.Min(_hitPoints + heal, _maxHitPoints);
            ValueChanged?.Invoke(_hitPoints, _maxHitPoints);
        }
    }

    private bool IsPositiveValue(float value) 
    {
        return value > 0;
    }
}
