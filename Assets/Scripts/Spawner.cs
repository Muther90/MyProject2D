using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private T _prefab;
    [SerializeField] private PointManager _pointManager;

    private ObjectPool<T> _pool; 
    private Vector2[] _spawnPoints;
    private Queue<Vector2> _queueToSpawn = new Queue<Vector2>();
    private WaitForSeconds _waitForSeconds;
    private Coroutine _countdownRoutine;

    private void Awake()
    {
        if (_pointManager == null)
        {
            throw new System.Exception("Places of spawn is not assigned!");
        }

        _spawnPoints = _pointManager.Get();
        _waitForSeconds = new WaitForSeconds(_cooldown);

        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab, Vector2.zero, Quaternion.identity),
            actionOnGet: (obj) => OnGetObject(obj),
            actionOnRelease: (obj) => OnReleaseObject(obj),
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
        foreach (Vector2 point in _spawnPoints)
        {
            _queueToSpawn.Enqueue(point);
        }

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _pool.Get();
        }
    }

    private IEnumerator CountdownRoutine()
    {
        while (_queueToSpawn.Count > 0)
        {
            yield return _waitForSeconds;

            _pool.Get();
        }

        _countdownRoutine = null;
    }

    private void OnGetObject(T obj)
    {
        if (_queueToSpawn.Count > 0)
        {
            obj.transform.position = _queueToSpawn.Dequeue();
            obj.Taken += ReleaseObject; 
            obj.gameObject.SetActive(true);
        }
    }

    private void ReleaseObject(ISpawnable obj)
    {
        T typedObj = (T)obj;
        _pool.Release(typedObj);

        if (_countdownRoutine == null && _queueToSpawn.Count > 0)
        {
            _countdownRoutine = StartCoroutine(CountdownRoutine());
        }
    }

    private void OnReleaseObject(T obj)
    {
        _queueToSpawn.Enqueue((Vector2)obj.transform.position);
        obj.gameObject.SetActive(false);
        obj.Taken -= ReleaseObject;
    }
}
