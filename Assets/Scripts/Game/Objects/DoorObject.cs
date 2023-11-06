using System;
using Game.CallbackObjects;
using Game.Services;
using Game.UI;
using Scopes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Objects
{
    public class DoorObject : MonoBehaviour, IInteractable, IInjectable
    {
        [SerializeField] private CanvasGroup _canInteractIndicator;

        [SerializeField] private string _keyToOpen;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private CallbackObject _onOpened;
        
        [Inject] private InventoryUI _inventory;
        [Inject] private InteractService _interactService;
        [Inject] private IObjectResolver _resolver;
        
        private bool _open;

        private void Start()
        {
            _onOpened = _resolver.Instantiate(_onOpened);
        }

        public void Interact()
        {
            if (_inventory.HasItem(_keyToOpen))
            {
                _open = true;
                _inventory.RemoveItem(_keyToOpen);
                _onOpened.Callback();
            }
            else
            {
                Debug.Log("You need a key to open this door.");
            }

            _collider.enabled = !_open;
        }

        private void Update()
        {
            var b = !_open && ReferenceEquals(_interactService.CurrentInteractable, this);
            _canInteractIndicator.alpha = b ? 1 : 0;
        }
    }
}