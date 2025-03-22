using UnityEngine;

public class AidKit : Item
{
    [SerializeField] private int _healAmount = 10;

    protected override void ApplyEffect(Collector collector)
    {
        if (collector.TryGetComponent<Health>(out Health health))
        {
            health.TakeHeal(_healAmount);
        }
    }
}
