using Game.CallbackObjects;
using Game.Services;
using Game.UI;
using Scopes;
using UnityEngine;
using VContainer;

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

        private bool _open;
        
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