using System;
using UnityEngine;

public abstract class Item : MonoBehaviour, ISpawnable
{
    public event Action<ISpawnable> Taken;

    public void Collect(Collector collector)
    {
        ApplyEffect(collector);
        Taken?.Invoke(this);
    }

    protected abstract void ApplyEffect(Collector collector);
}
