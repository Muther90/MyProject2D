using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _intervalDamage;

    private Coroutine _damageCoroutine;
    private WaitForSeconds _waitForSeconds;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out IDamageable damageable) &&  _damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(DamageCoroutine(damageable));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out _) && _damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }

    private IEnumerator DamageCoroutine(IDamageable target)
    {
        _waitForSeconds = new WaitForSeconds(_intervalDamage);

        while (enabled && (target as UnityEngine.Object) != null)
        {
            target.TakeDamage(_damage);
            yield return _waitForSeconds;
        }

        _damageCoroutine = null;
    }
}
