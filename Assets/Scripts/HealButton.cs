using UnityEngine;

public class HealButton : PressButton
{
    [SerializeField] private int _healAmount = 10;

    protected override void ApplyEffect(Collider2D collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            player.ApplyHeal(_healAmount);
        }
    }
}
