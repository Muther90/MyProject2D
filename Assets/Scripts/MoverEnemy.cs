using UnityEngine;

public class MoverEnemy : MonoBehaviour
{
    [SerializeField] private PointManager _pointManager;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToTarget;

    private Vector2[] _waypoints;
    private Vector2 _currentTarget;
    private int _waypointIndex = 0;
    private float _squaredDistanceToTarget;

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
        _currentTarget = _waypoints[_waypointIndex];
    }

    private void Update()
    {
        Vector2 currentPosition = (Vector2)transform.position;
        currentPosition = Vector2.MoveTowards(currentPosition, _currentTarget, _speed * Time.deltaTime);
        transform.position = currentPosition;

        if ((currentPosition - _currentTarget).sqrMagnitude <= _squaredDistanceToTarget)
        {
            UpdateTarget();
        }
    }

    private void UpdateTarget()
    {
        _waypointIndex = ++_waypointIndex % _waypoints.Length;
        _currentTarget = _waypoints[_waypointIndex];
    }
}
