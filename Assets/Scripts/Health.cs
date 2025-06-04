using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float, float> HealthChanged;

    [SerializeField] private float _maxHitPoints;
    [SerializeField] float _hitPoints;

    private void Start()
    {
        HealthChanged?.Invoke(_hitPoints, _maxHitPoints);
    }

    public void ApplyDamage(float damage)
    {
        _hitPoints -= damage;
        HealthChanged?.Invoke(_hitPoints, _maxHitPoints);

        if (_hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyHeal(float heal)
    {
        _hitPoints = Mathf.Min(_hitPoints + heal, _maxHitPoints);
        HealthChanged?.Invoke(_hitPoints, _maxHitPoints);
    }
}
