using UnityEngine;

public class DamageButton : ButtonHandler
{
    [SerializeField] private float _damage;

    protected override void ApplyEffect()
    {
        _health.ApplyDamage(_damage);
    }
}
