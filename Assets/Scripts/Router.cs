using System.Collections;
using UnityEngine;

public class Router : MonoBehaviour
{
    [SerializeField] private WaypointProvider _pointManager;
    [SerializeField] private float _distanceToTarget;
    [SerializeField] private float _sensitivity = 0.2f;

    private Vector2[] _waypoints;
    private int _waypointIndex = 0;
    private float _squaredDistanceToTarget;
    private Coroutine _directionToTargetRoutine;
    private WaitForSeconds _waitForSeconds;

    public Vector2 CurrentTarget { get; private set; }

    private void Awake()
    {
        if (_pointManager == null)
        {
            throw new System.Exception("Route is not assigned!");
        }
    }

    private void Start()
    {
        _waypoints = _pointManager.Get();
        _squaredDistanceToTarget = _distanceToTarget * _distanceToTarget;
        CurrentTarget = _waypoints[_waypointIndex];

        _directionToTargetRoutine = StartCoroutine(DirectionToTargetRoutine());
    }

    private void OnDestroy()
    {
        if (_directionToTargetRoutine != null)
        {
            StopCoroutine(_directionToTargetRoutine);
        }
    }

    private IEnumerator DirectionToTargetRoutine()
    {
        _waitForSeconds = new WaitForSeconds(_sensitivity);

        while (enabled)
        {
            if (((Vector2)transform.position - CurrentTarget).sqrMagnitude <= _squaredDistanceToTarget)
            {
                UpdateTarget();
            }

            yield return _waitForSeconds;
        }
    }

    private void UpdateTarget()
    {
        _waypointIndex = ++_waypointIndex % _waypoints.Length;
        CurrentTarget = _waypoints[_waypointIndex];
    }
}
