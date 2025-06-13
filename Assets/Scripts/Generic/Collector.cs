using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Item>(out Item item))
        {
            item.Collect(this);
        }
    }
}
