using UnityEngine;

public class Enemy : MonoBehaviour , IDamagable
{
    [SerializeField] private int _hitPoint = 20;
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _damageCooldown = 1.0f;
    [SerializeField] private Mover _mover;
    [SerializeField] private Router _router;
    [SerializeField] private Viewer _viewer;

    private float _damageTimer = 0f;
    private Vector2 _directionToTarget;

    private void FixedUpdate()
    {
        if (_viewer.IsPlayerDetected)
        {
            _directionToTarget = (_viewer.PlayerPosition - (Vector2)transform.position).normalized;
            _viewer.View(_directionToTarget);
            _mover.Move(Mathf.Sign(_viewer.PlayerPosition.x - transform.position.x));
        }
        else
        {
            _directionToTarget = (_router.CurrentTarget - (Vector2)transform.position).normalized;
            _viewer.View(_directionToTarget);
            _mover.Move(Mathf.Sign(_router.CurrentTarget.x - transform.position.x));
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
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject); 
    }
}
