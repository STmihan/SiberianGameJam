using Game.Objects;
using Game.Services;
using UnityEngine;
using VContainer;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _interactRadius;

        [Inject]
        private InputManager _inputManager;
        [Inject]
        private InteractService _interactService;
        
        private Rigidbody2D _rb;
        private PlayerAnimationController _animationController;
        private Animator _animator;
        private bool _isMoving;
        private Vector2 _movementInput;

        private Vector2 _lastDirection = Vector2.down;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _animationController = new PlayerAnimationController(_animator, _lastDirection);
        }

        private void Update()
        {
            Move();
            CheckInteract();
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                Vector2 movement = _movementInput * (_moveSpeed * Time.fixedDeltaTime);
                _rb.MovePosition(_rb.position + movement);
            }
        }

        private void Move()
        {
            _movementInput = _inputManager.GetMovementInput().normalized;
            _isMoving = _movementInput != Vector2.zero;
            _animationController.Update(_isMoving, _movementInput, _lastDirection);
            if (_isMoving)
            {
                _lastDirection = _movementInput;
            }
        }

        private void CheckInteract()
        {
            Vector2 interactPosition = transform.position;
            Collider2D[] hits = new Collider2D[4];
            int hitCount = Physics2D.OverlapCircleNonAlloc(interactPosition, _interactRadius, hits);
            if (hitCount > 0)
            {
                for (int i = 0; i < hitCount; i++)
                {
                    if (hits[i].TryGetComponent(out IInteractable interactable))
                    {
                        _interactService.CurrentInteractable = interactable;
                        if (_inputManager.GetInteractInput())
                        {
                            interactable.Interact();
                        }
                    }
                    else
                    {
                        _interactService.CurrentInteractable = null;
                    }
                }
            }
            else
            {
                _interactService.CurrentInteractable = null;
            }
        }
    }
}