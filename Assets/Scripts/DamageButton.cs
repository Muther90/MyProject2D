using UnityEngine;

public class DamageButton : PressButton
{
    [SerializeField] private int _damageAmount = 10;

    protected override void ApplyEffect(Collider2D collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            player.ApplyDamage(_damageAmount);
        }
    }
}

