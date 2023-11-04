using UnityEngine;

namespace Game
{
    public class PlayerAnimationController
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
        private static readonly int LastVertical = Animator.StringToHash("LastVertical");

        private readonly Animator _animator;

        public PlayerAnimationController(Animator animator, Vector2 lastDirection)
        {
            _animator = animator;
            _animator.SetFloat(LastHorizontal, lastDirection.x);
            _animator.SetFloat(LastVertical, lastDirection.y);
        }

        public void Update(bool isMoving, Vector2 direction, Vector2 lastDirection)
        {
            if (isMoving)
            {
                _animator.SetFloat(Horizontal, direction.x);
                _animator.SetFloat(Vertical, direction.y);
                _animator.SetBool(IsMoving, true);
                _animator.SetFloat(LastHorizontal, lastDirection.x);
                _animator.SetFloat(LastVertical, lastDirection.y);
            }
            else
            {
                _animator.SetBool(IsMoving, false);
            }
        }
    }
}