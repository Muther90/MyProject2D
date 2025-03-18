using System;
using UnityEngine;

public class Item : MonoBehaviour, ISpawnable
{
    public event Action<ISpawnable> Taken;

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Player>(out Player player))
        {
            ApplyEffect(player); 
            Taken?.Invoke(this); 
        }
    }

    protected virtual void ApplyEffect(Player player)
    {

    }
}
