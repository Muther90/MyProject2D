using UnityEngine;

public class Player : MonoBehaviour, IHealth
{
    [SerializeField] private int _hitPoint;
    [SerializeField] private int _damage;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private Animator _animator;

    private PlayerAnimator _playerAnimator;
    private float _direction;
    private bool _isMoving;
    private bool _isHitting;

    private void Awake()
    {
        _playerAnimator = new PlayerAnimator(_animator);
    }

    private void FixedUpdate()
    {
        _direction = _inputReader.Direction;
        _isMoving = Mathf.Approximately(_direction, 0f) == false;
        _playerAnimator.SetIsMoving(_isMoving);

        if (_isMoving)
        {
            _playerAnimator.SetMoveDirection(_direction);
            _mover.Move(_direction);

            if (_inputReader.GetIsHit())
            {
                _playerAnimator.TriggerHit();
                _isHitting = true;
            }
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _jumper.Jump();
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
            Destroy(gameObject);
        }
    }
}
