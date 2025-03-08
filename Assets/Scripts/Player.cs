using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Animator _animator;

    private void FixedUpdate()
    {
        _animator.SetFloat("MoveDirection", _inputReader.Direction);

        if (_inputReader.Direction != 0)
        {
            _animator.SetBool("isMove", true);
            _animator.SetFloat("MoveDirection", _inputReader.Direction);
            _mover.Move(_inputReader.Direction);
        }

        if (_inputReader.Direction == 0)
        {
            _animator.SetBool("isMove", false);
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _mover.Jump();
        }
    }
}
