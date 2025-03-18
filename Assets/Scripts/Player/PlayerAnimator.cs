using UnityEngine;

public class PlayerAnimator
{
    private readonly Animator _animator;

    public PlayerAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void SetMoveDirection(float direction)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.MoveDirection, direction);
    }

    public void SetIsMoving(bool isMoving)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsMove, isMoving);
    }

    public void TriggerHit()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Hit);
    }
}
