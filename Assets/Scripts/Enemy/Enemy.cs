using UnityEngine;

public class Enemy : MonoBehaviour , IHealth
{
    [SerializeField] private int _hitPoint = 20;
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _damageCooldown = 1.0f;
    [SerializeField] private Mover _mover;
    [SerializeField] private Router _router;
    [SerializeField] private Vision _viewer;

    private float _damageTimer = 0f;
    private Vector2 _directionToTarget;

    private void FixedUpdate()
    {
        if (_viewer.IsPlayerDetected)
        {
            MoveTo(_viewer.PlayerPosition);
        }
        else
        {
            MoveTo(_router.CurrentTarget);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_viewer.IsPlayerDetected && collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _damageTimer += Time.deltaTime;

            if (_damageTimer >= _damageCooldown)
            {
                player.TakeDamage(_damage);
                _damageTimer = 0f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player _))
        {
            _damageTimer = 0f;
        }
    }

    public void TakeHeal(int amount)
    {
        _hitPoint += amount;
    }

    public void TakeDamage(int amount)
    {
        _hitPoint -= amount;

        if (_hitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void MoveTo(Vector2 position)
    {
        _directionToTarget = (position - (Vector2)transform.position).normalized;
        _viewer.LookAt(_directionToTarget);
        _mover.Move(Mathf.Sign(position.x - transform.position.x));
    }
}
