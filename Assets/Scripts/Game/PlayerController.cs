using Game.Services;
using UnityEngine;
using Utils;
using VContainer;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : SceneScopeSingleton<PlayerController>
    {
        public IInteractable CurrentInteractable { get; private set; }

        [SerializeField] private float _moveSpeed;

        [Inject]
        private InputManager _inputManager;
        
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
            Vector2 interactPosition = transform.position + (Vector3)(_lastDirection * 0.5f);
            Collider2D[] hits = new Collider2D[4];
            int hitCount = Physics2D.OverlapCircleNonAlloc(interactPosition, 0.5f, hits);
            if (hitCount > 0)
            {
                for (int i = 0; i < hitCount; i++)
                {
                    if (hits[i].TryGetComponent(out IInteractable interactable))
                    {
                        CurrentInteractable = interactable;
                        if (_inputManager.GetInteractInput())
                        {
                            interactable.Interact();
                        }
                    }
                    else
                    {
                        CurrentInteractable = null;
                    }
                }
            }
            else
            {
                CurrentInteractable = null;
            }
        }
    }
}