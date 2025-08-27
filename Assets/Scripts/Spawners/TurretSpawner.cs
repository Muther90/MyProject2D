using System.Collections;
using UnityEngine;

public class TurretSpawner : BaseObjectPool<Turret>
{
    [SerializeField] private float _delay;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;

    private Coroutine _generateCoroutine;
    private float _spawnPositionY;
    private Vector3 _spawnPoint;

    private void OnEnable()
    {
        _generateCoroutine = StartCoroutine(GenerateTurretCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_generateCoroutine);
        _generateCoroutine = null;
    }

    private void Start()
    {
        Reset();
    }

    private IEnumerator GenerateTurretCoroutine()
    {
        WaitForSeconds wait = new (_delay);

        while (enabled)
        {
            Spawn();

            yield return wait;
        }
    }

    private void Spawn()
    {
        _spawnPositionY = Random.Range(_lowerBound, _upperBound);
        _spawnPoint = new (transform.position.x, _spawnPositionY, 0f);

        Spawn(_spawnPoint, Quaternion.identity);
    }
}