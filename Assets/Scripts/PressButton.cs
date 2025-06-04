using UnityEngine;

public abstract class PressButton : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ApplyEffect(collider);
    }

    protected abstract void ApplyEffect(Collider2D collider);
}
