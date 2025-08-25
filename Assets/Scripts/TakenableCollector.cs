using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TakenableCollector : MonoBehaviour
{
    private Collider2D _collider2D;

    private void OnValidate()
    {
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ITakenable takenable))
        {
            takenable.CallTaken();
        }
    }
}