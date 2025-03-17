using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private int _hitPoint;
    [SerializeField] private int _damage;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Animator _animator;

    public const string MoveDirection = "MoveDirection";
    public const string IsMove = "IsMove";
    public const string Hit = "Hit";

    private float _direction;
    private bool _isMoving;
    private bool _isHitting;

    private void FixedUpdate()
    {
        _direction = _inputReader.Direction;
        _isMoving = Mathf.Approximately(_direction, 0f) == false;
        _animator.SetBool(IsMove, _isMoving);

        if (_isMoving)
        {
            _animator.SetFloat(MoveDirection, _direction);
            _mover.Move(_direction);

            if (_inputReader.GetIsHit())
            {
                _animator.SetTrigger(Hit);
                _isHitting = true;
            }
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _mover.Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isHitting && collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            _isHitting = false;
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
