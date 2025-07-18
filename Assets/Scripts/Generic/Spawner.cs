using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private float _interval;
    [SerializeField] private T _prefab;
    [SerializeField] private WaypointProvider _pointManager;

    private ObjectPool<T> _pool; 
    private Vector2[] _spawnPoints;
    private Queue<Vector2> _queueToSpawn = new();
    private Coroutine _countdownCoroutine;

    private void Awake()
    {
        if (_pointManager == null)
        {
            throw new System.Exception("Places of spawn is not assigned!");
        }

        _spawnPoints = _pointManager.Get(); 

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

    private IEnumerator CountdownCoroutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_interval);

        while (_queueToSpawn.Count > 0)
        {
            _pool.Get();

            yield return waitForSeconds;
        }

        _countdownCoroutine = null;
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

        if (_countdownCoroutine == null && _queueToSpawn.Count > 0)
        {
            _countdownCoroutine = StartCoroutine(CountdownCoroutine());
        }
    }

    private void OnReleaseObject(T obj)
    {
        _queueToSpawn.Enqueue((Vector2)obj.transform.position);
        obj.gameObject.SetActive(false);
        obj.Taken -= ReleaseObject;
    }
}
