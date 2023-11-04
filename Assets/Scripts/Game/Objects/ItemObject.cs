using Game.Data;
using Game.Services;
using Game.UI;
using UnityEngine;
using VContainer;

namespace Game.Objects
{
    public class ItemObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private CanvasGroup _canInteractIndicator;
        [SerializeField] private Item _item;
        
        [Inject] private InteractService _interactService;
        [Inject] private InventoryUI _inventory;
        
        public void Interact()
        {
            _inventory.AddItem(_item.Key);
            Destroy(gameObject);
        }

        private void Update()
        {
            _canInteractIndicator.alpha = ReferenceEquals(_interactService.CurrentInteractable, this) ? 1 : 0;
        }
    }
}