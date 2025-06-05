using UnityEngine;

public class HealButton : ButtonHandler
{
    [SerializeField] private float _heal;

    protected override void ApplyEffect()
    {
        _health.ApplyHeal(_heal);
    }
}
