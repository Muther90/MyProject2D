using System;
using System.Collections;
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

    private Coroutine _damagingCoroutine;
    private Coroutine _spellCoroutine;

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
        yield return _damagingCoroutine = StartCoroutine(DamagingCoroutine());
        CastEnded?.Invoke();

        IntervalStarted?.Invoke(_interval); 
        yield return new WaitForSeconds(_interval);
        IntervalEnded?.Invoke(); 

        _spellCoroutine = null;
    }

    private IEnumerator DamagingCoroutine()
    {
        float startTime = Time.time;
        float endTime = startTime + _duration;
        float radius = _collider.bounds.extents.x;

        while (Time.time < endTime)
        {
            IDamageable target = TargetFinder.FindNearestDamageable(transform, radius);

            if (target != null)
            {
                target.TakeDamage(_damage);
                _health.ApplyHeal(_heal);
            }

            yield return new WaitForSeconds(Mathf.Min(_damageTickRate, endTime - Time.time));
        }

        _damagingCoroutine = null;
    }
}
