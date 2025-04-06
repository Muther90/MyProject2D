using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int MoveDirection = Animator.StringToHash("MoveDirection");
        public static readonly int IsMove = Animator.StringToHash("IsMove");
        public static readonly int Hit = Animator.StringToHash("Hit");
    }
}
