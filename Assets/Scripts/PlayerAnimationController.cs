using UnityEngine;

public class PlayerAnimationController
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
    private static readonly int LastVertical = Animator.StringToHash("LastVertical");

    private readonly Animator _animator;

    private Vector2 _lastDirection = Vector2.down;

    public PlayerAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void Update(Vector2 direction, bool isMoving)
    {
        if (isMoving)
        {
            _lastDirection = direction;
            _animator.SetFloat(Horizontal, direction.x);
            _animator.SetFloat(Vertical, direction.y);
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(LastHorizontal, _lastDirection.x);
            _animator.SetFloat(LastVertical, _lastDirection.y);
        }
        else
        {
            _animator.SetBool(IsMoving, false);
        }
    }
}