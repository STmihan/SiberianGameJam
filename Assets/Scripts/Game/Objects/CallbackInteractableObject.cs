using Game.Services;
using Scopes;
using UnityEngine;
using VContainer;

namespace Game.Objects
{
    public class CallbackInteractableObject : MonoBehaviour, IInteractable, IInjectable
    {
        [SerializeField] private CanvasGroup _canInteractIndicator;

        [Inject] private InteractService _interactService;
        
        private bool _opened;

        public void Interact()
        {
            if (!_opened && ReferenceEquals(_interactService.CurrentInteractable, this))
                _canInteractIndicator.alpha = 1;
            else
                _canInteractIndicator.alpha = 0;
        }
    }
}