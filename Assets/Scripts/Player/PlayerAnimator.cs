using UnityEngine;

public class PlayerAnimator
{
    private readonly Animator _animator;

    public PlayerAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void SetMoveDirection(bool isMoving, float direction)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsMove, isMoving);
        _animator.SetFloat(PlayerAnimatorData.Params.MoveDirection, direction);
    }
}
