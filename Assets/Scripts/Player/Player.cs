using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(InputReader))]
public class Player : MonoBehaviour, IHealth
{
    [SerializeField] private Health _health;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Animator _animator;
    [SerializeField] private Spell _spell;

    private PlayerAnimator _playerAnimator;
    private float _direction;
    private bool _isMoving;

    private void Awake()
    {
        _playerAnimator = new PlayerAnimator(_animator);
    }

    private void OnEnable()
    {
        _inputReader.SpaceKeyDowned += _spell.Cast;
    }

    private void OnDisable()
    {
        _inputReader.SpaceKeyDowned -= _spell.Cast; 
    }

    private void FixedUpdate()
    {
        _direction = _inputReader.Direction;
        _isMoving = Mathf.Approximately(_direction, 0f) == false;

        if (_isMoving)
        {
            _playerAnimator.SetMoveDirection(_isMoving, _direction);
            _mover.Move(_direction);
        }
    }

    public void TakeDamage(float damage) => _health.ApplyDamage(damage);
    public void TakeHeal(float heal) => _health.ApplyHeal(heal);
}
