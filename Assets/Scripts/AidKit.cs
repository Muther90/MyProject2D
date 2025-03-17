using UnityEngine;

public class AidKit : Pickup
{
    [SerializeField] private int _healAmount = 10;

    protected override void ApplyEffect(Player player)
    {
        player.TakeHeal(_healAmount);
    }
}
