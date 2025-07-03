using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class SpellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SmoothSlider _smoothSlider;

    private void Awake()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _smoothSlider.gameObject.SetActive(false);
    }

    public IEnumerator ViewDuration(float duration) => _smoothSlider.Draining(duration);
    public IEnumerator ViewInterval(float interval) => _smoothSlider.Filling(interval);

    public void EnableAura() 
    { 
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }

    public void DisableAura()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }

    public void EnableSlider()
    {
        _smoothSlider.gameObject.SetActive(true);
    }

    public void DisableSlider()
    {
        _smoothSlider.gameObject.SetActive(false);
    }
}
