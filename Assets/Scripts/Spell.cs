using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField, Min(0f)] private float _duration = 6f;
    [SerializeField, Min(0f)] private float _interval = 4f;
    [SerializeField, Min(0f)] private float _damage = 10f;
    [SerializeField, Min(0f)] private float _damageTickRate = 1f;
    [SerializeField, Min(0f)] private float _heal = 5f;
    [SerializeField] private Health _health;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SmoothSlider _smoothSlider;

    private Coroutine _damageCoroutine;
    private Coroutine _spellCoroutine;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_damageCoroutine == null && collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            _damageCoroutine = StartCoroutine(DamageCoroutine(damageable));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (_damageCoroutine != null && collider.TryGetComponent<IDamageable>(out _))
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }

    private void OnClick()
    {
        if (_spellCoroutine != null)
        {
            StopCoroutine(_spellCoroutine);
            _spellCoroutine = null;
        }

        _spellCoroutine = StartCoroutine(SpellCoroutine());
    }

    private IEnumerator SpellCoroutine()
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;

        _button.gameObject.SetActive(false);
        _smoothSlider.gameObject.SetActive(true);

        yield return StartCoroutine(_smoothSlider.Draining(_duration));

        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }

        _spriteRenderer.enabled = false;
        _collider.enabled = false;

        yield return StartCoroutine(_smoothSlider.Filling(_interval));

        _smoothSlider.gameObject.SetActive(false);
        _button.gameObject.SetActive(true);

        _spellCoroutine = null;
    }

    private IEnumerator DamageCoroutine(IDamageable target)
    {
        _waitForSeconds = new WaitForSeconds(_damageTickRate);

        while (enabled && (target as UnityEngine.Object) != null)
        {
            target.TakeDamage(_damage);
            _health.ApplyHeal(_heal);

            yield return _waitForSeconds;
        }

        _damageCoroutine = null;
    }
}
