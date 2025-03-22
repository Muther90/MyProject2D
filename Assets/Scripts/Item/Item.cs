using System;
using UnityEngine;

public class Item : MonoBehaviour, ISpawnable
{
    public event Action<ISpawnable> Taken;

    public void Collect(Collector collector)
    {
        ApplyEffect(collector);
        Taken?.Invoke(this);
    }

    protected virtual void ApplyEffect(Collector collector)
    {

    }
}
