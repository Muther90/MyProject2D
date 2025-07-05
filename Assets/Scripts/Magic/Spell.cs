using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _duration = 6f;
    [SerializeField, Min(0f)] private float _interval = 4f;
    [SerializeField, Min(0f)] private float _damage = 10f;
    [SerializeField, Min(0f)] private float _damageTickRate = 1f;
    [SerializeField, Min(0f)] private float _heal = 5f;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Health _health;
    [SerializeField] private SpellView _spellView;

    private Coroutine _damageCoroutine;
    private Coroutine _spellCoroutine;
    private Collider2D[] _overlapColliders = new Collider2D[100];

    public event Action<float> CastStarted;
    public event Action CastEnded;
    public event Action<float> IntervalStarted;
    public event Action IntervalEnded;

    public void Cast()
    {
        if (_spellCoroutine == null)
        {
            _spellCoroutine = StartCoroutine(SpellCoroutine());
        }
    }

    private IEnumerator SpellCoroutine()
    {
        CastStarted?.Invoke(_duration);

        _damageCoroutine = StartCoroutine(DamagingCoroutine());
        yield return new WaitForSeconds(_duration);

        StopCoroutine(_damageCoroutine);
        _damageCoroutine = null;

        CastEnded?.Invoke();

        IntervalStarted?.Invoke(_interval); 
        yield return new WaitForSeconds(_interval);
        IntervalEnded?.Invoke(); 

        _spellCoroutine = null;
    }

    private IEnumerator DamagingCoroutine()
    {
        Vector2 transformPosition;
        WaitForSeconds waitForSeconds = new(_damageTickRate);
        float radius = _collider.bounds.extents.x;
        int countColliders;

        while (enabled)
        {
            transformPosition = transform.position;
            countColliders = Physics2D.OverlapCircleNonAlloc(transformPosition, radius, _overlapColliders);

            IDamageable target = _overlapColliders
                .Take(countColliders) 
                .Select(collider => new
                {
                    Collider = collider,
                    DamageableTarget = collider.TryGetComponent<IDamageable>(out var damageable) ? damageable : null
                })
                .Where(potentialTarget => potentialTarget.DamageableTarget != null)
                .OrderBy(potentialTarget => ((Vector2)potentialTarget.Collider.transform.position - transformPosition).sqrMagnitude)
                .Select(potentialTarget => potentialTarget.DamageableTarget)
                .FirstOrDefault();

            if (target != null)
            {
                target.TakeDamage(_damage);
                _health.ApplyHeal(_heal);
            }

            yield return waitForSeconds;
        }
    }
}
