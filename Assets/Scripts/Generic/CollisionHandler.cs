using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class CollisionHandler<T> : MonoBehaviour where T : class
{
    private Collider2D _collider2D;

    public event System.Action<T> CollisionDetected;

    private void OnValidate()
    {
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out T component))
        {
            CollisionDetected?.Invoke(component);
        }
    }
}