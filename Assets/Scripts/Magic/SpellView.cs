using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class SpellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SmoothSlider _smoothSlider;
    [SerializeField] private Spell _spell;

    private void Awake()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _smoothSlider.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _spell.CastStarted += OnCastStarted;
        _spell.CastEnded += OnCastEnded;
        _spell.IntervalStarted += OnIntervalStarted;
        _spell.IntervalEnded += OnIntervalEnded;
    }

    private void OnDisable()
    {
        _spell.CastStarted -= OnCastStarted;
        _spell.CastEnded -= OnCastEnded;
        _spell.IntervalStarted -= OnIntervalStarted;
        _spell.IntervalEnded -= OnIntervalEnded;
    }

    private void OnCastStarted(float duration)
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
        _smoothSlider.gameObject.SetActive(true);
        _smoothSlider.Draining(duration);
    }

    private void OnCastEnded()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }

    private void OnIntervalStarted(float interval)
    {
        _smoothSlider.Filling(interval);
    }

    private void OnIntervalEnded()
    {
        _smoothSlider.gameObject.SetActive(false);
    }
}