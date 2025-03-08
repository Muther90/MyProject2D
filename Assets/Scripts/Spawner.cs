using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private Coin _coin;
    [SerializeField] private PointManager _pointManager;

    private ObjectPool<Coin> _coins;
    private WaitForSeconds _waitForSeconds;
    private Vector2[] _spawnPoints;
    private Queue<Vector2> queueToSpawn = new Queue<Vector2>();
    private Coroutine _countdownCoroutine;

    private void Awake()
    {
        if (_pointManager == null)
        {
            throw new System.Exception("Places of spawn is not assigned!");
        }

        _spawnPoints = _pointManager.Get();

        _coins = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_coin, Vector2.zero, Quaternion.identity),
            actionOnGet: (obj) => OnGetCoin(obj),
            actionOnRelease: (obj) => OnReleaseCoin(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _spawnPoints.Length,
            maxSize: _spawnPoints.Length);
    }

    private void Start()
    {
        FirstSpawn();
    }

    private void FirstSpawn()
    {
        foreach (var point in _spawnPoints)
        {
            queueToSpawn.Enqueue(point);
        }

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _coins.Get();
        }
    }

    private IEnumerator Countdown()
    {
        _waitForSeconds = new WaitForSeconds(_cooldown);

        while (queueToSpawn.Count > 0)
        {
            yield return _waitForSeconds;

            _coins.Get();
        }

        _countdownCoroutine = null;
    }

    private void OnGetCoin(Coin coin)
    {
        if (queueToSpawn.Count > 0)
        {
            coin.transform.position = queueToSpawn.Dequeue();
            coin.Taken += ReleaseCoin;
            coin.gameObject.SetActive(true);
        }
    }

    private void ReleaseCoin(Coin coin)
    {
        _coins.Release(coin);

        if (_countdownCoroutine == null && queueToSpawn.Count > 0)
        {
            _countdownCoroutine = StartCoroutine(Countdown());
        }
    }

    private void OnReleaseCoin(Coin coin)
    {
        queueToSpawn.Enqueue((Vector2)coin.transform.position);
        coin.gameObject.SetActive(false);
        coin.Taken -= ReleaseCoin;
    }
}
