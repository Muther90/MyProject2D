using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _duration = 6f;
    [SerializeField, Min(0f)] private float _interval = 4f;
    [SerializeField, Min(0f)] private float _damage = 10f;
    [SerializeField, Min(0f)] private float _damageTickRate = 1f;
    [SerializeField, Min(0f)] private float _heal = 5f;
    [SerializeField] private Health _health;
    [SerializeField] private SpellView _spellView;

    private List<IDamageable> _targets = new();
    private Coroutine _damageCoroutine;
    private Coroutine _spellCoroutine;

    private void OnEnable()
    {
        InputReader.SpaceKeyDowned += OnKeyDown;
    }

    private void OnDisable()
    {
        InputReader.SpaceKeyDowned -= OnKeyDown;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_spellCoroutine != null && collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            _targets.Add(damageable);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            _targets.Remove(damageable);
        }
    }

    private void OnKeyDown()
    {
        if (_spellCoroutine == null )
        {
            _spellCoroutine = StartCoroutine(SpellCoroutine());
        }
    }

    private IEnumerator SpellCoroutine()
    {
        _spellView.EnableAura();
        _spellView.EnableSlider();

        _damageCoroutine = StartCoroutine(DamagingCoroutine());
        yield return StartCoroutine(_spellView.ViewDuration(_duration));

        StopCoroutine(_damageCoroutine);
        _damageCoroutine = null;
        _targets.Clear();

        _spellView.DisableAura();
        yield return StartCoroutine(_spellView.ViewInterval(_interval));
        _spellView.DisableSlider();

        _spellCoroutine = null;
    }

    private IEnumerator DamagingCoroutine()
    {
        WaitForSeconds waitForSeconds = new(_damageTickRate);

        while (enabled)
        {
            if (_targets.Any())
            {
                IDamageable DamageableTarget = _targets.OfType<Component>()
                    .OrderBy(component => Vector2.SqrMagnitude((Vector2)component.transform.position - (Vector2)transform.position))
                    .Select(component => component as IDamageable)
                    .FirstOrDefault();

                DamageableTarget.TakeDamage(_damage);
                _health.ApplyHeal(_heal);
            }

            yield return waitForSeconds;
        }
    }
}
