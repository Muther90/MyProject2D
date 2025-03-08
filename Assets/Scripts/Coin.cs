using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Taken;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            Taken?.Invoke(this);
        }
    }
}
