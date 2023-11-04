using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rb;
    private InputManager _inputManager;
    private PlayerAnimationController _animationController;
    private Animator _animator;
    private bool _isMoving;
    private Vector2 _movementInput;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _animationController = new PlayerAnimationController(_animator);
        _inputManager = new InputManager();
    }

    private void Update()
    {
        _movementInput = _inputManager.GetMovementInput().normalized;
        _isMoving = _movementInput != Vector2.zero;
        _animationController.Update(_movementInput, _isMoving);
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Vector2 movement = _movementInput * (_moveSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + movement);
        }
    }
}