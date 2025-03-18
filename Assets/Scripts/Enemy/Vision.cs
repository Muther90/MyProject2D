using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _angle;

    private float _angleToTarget;
    private float _halfAngle;

    public bool IsPlayerDetected { get; private set; }
    public Vector2 PlayerPosition { get; private set; }

    private void Awake()
    {
        _halfAngle = _angle / 2;
    }

    public void LookAt(Vector2 direction)
    {
        IsPlayerDetected = false; 

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

        for (int i = 0; i < colliders.Length && IsPlayerDetected == false; i++)
        {
            Collider2D collider = colliders[i];

            if (collider.TryGetComponent<Player>(out Player player))
            {
                PlayerPosition = (player.transform.position - transform.position).normalized;
                _angleToTarget = Vector2.Angle(direction, PlayerPosition);

                if (_angleToTarget < _halfAngle)
                {
                    RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position);

                    if (hit.collider != null && hit.collider.TryGetComponent<Player>(out _))
                    {
                        IsPlayerDetected = true;
                        PlayerPosition = hit.transform.position;
                    }
                }
            }
        }
    }
}
