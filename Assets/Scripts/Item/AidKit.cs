using UnityEngine;

public class AidKit : Item
{
    [SerializeField] private int _healAmount = 10;

    protected override void ApplyEffect(Collector collector)
    {
        if (collector.TryGetComponent<IHealable>(out IHealable healTarget))
        {
            healTarget.TakeHeal(_healAmount);
        }
    }
}
