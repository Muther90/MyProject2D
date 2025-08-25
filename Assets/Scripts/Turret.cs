using System;
using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour, IPoolObject, ITakenable, IInteractable
{
    [SerializeField] BulletSpawner _bulletSpawner;
    [SerializeField] float _delay;

    private Coroutine _fireCoroutine;

    public event Action<IPoolObject> Taken;

    private void OnEnable()
    {
        _fireCoroutine = StartCoroutine(FireCoroutine());
    }

    private void OnDisable()
    {
        _bulletSpawner.Reset();
        StopCoroutine(_fireCoroutine);
        _fireCoroutine = null;
    }

    public void CallTaken()
    {
        Taken?.Invoke(this);
    }

    private IEnumerator FireCoroutine()
    {
        yield return null;

        WaitForSeconds wait = new(_delay);

        while (enabled)
        {
            _bulletSpawner.Fire();

            yield return wait;
        }
    }
}