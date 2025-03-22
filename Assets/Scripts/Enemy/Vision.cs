using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _angle;
    [SerializeField] private LayerMask _targetLayerMask;

    private float _angleToTarget;
    private float _halfAngle;
    private Transform _transform;
    private Vector2 _myPosition;
    private Vector2 _playerPosition;
    private int _numberColliders;
    private Collider2D[] _colliders = new Collider2D[1];
    private Collider2D _targetCollider;

    public bool IsPlayerDetected { get; private set; }
    public Vector2 PlayerPosition { get; private set; }

    private void Awake()
    {
        _transform = transform;
        _halfAngle = _angle / 2;
    }

    public void LookAt(Vector2 direction)
    {
        IsPlayerDetected = false;

        _myPosition = _transform.position;
        _numberColliders = Physics2D.OverlapCircleNonAlloc(_myPosition, _radius, _colliders, _targetLayerMask);

        if (_numberColliders > 0)
        {
            _targetCollider = _colliders[0];

            if (_targetCollider.TryGetComponent<Player>(out Player player))
            {
                _playerPosition = (Vector2)player.transform.position;
                PlayerPosition = _playerPosition - _myPosition; 
                _angleToTarget = Vector2.Angle(direction, PlayerPosition);

                if (_angleToTarget < _halfAngle)
                {
                    RaycastHit2D hit = Physics2D.Linecast(_myPosition, _playerPosition);

                    if (hit.collider == _targetCollider)
                    {
                        IsPlayerDetected = true;
                        PlayerPosition = hit.point;
                    }
                }
            }
        }
    }
}
