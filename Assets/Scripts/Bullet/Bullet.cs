using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IPoolObject, IInteractable, ITakenable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] BulletCollisionHandler _bulletCollisionHandler;

    private Rigidbody2D _rigidbody2D;
    private Coroutine _fireCoroutine;

    public event Action<IPoolObject> Taken;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _bulletCollisionHandler.CollisionDetected += ProcessCollision;
        _fireCoroutine = StartCoroutine(ExpireLifetimeCoroutine());
    }

    private void OnDisable()
    {
        _bulletCollisionHandler.CollisionDetected -= ProcessCollision;
        StopCoroutine(_fireCoroutine);
        _fireCoroutine = null;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.up * _speed;
    }

    public void CallTaken()
    {
        Taken?.Invoke(this);
    }

    private IEnumerator ExpireLifetimeCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);

        Taken?.Invoke(this);
    }

    private void ProcessCollision(ITakenable takenable)
    {
        takenable.CallTaken();
        CallTaken();
    }
}