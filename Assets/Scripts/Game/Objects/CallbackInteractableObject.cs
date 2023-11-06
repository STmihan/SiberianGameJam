using System;
using Game.CallbackObjects;
using Game.Services;
using Scopes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Objects
{
    public class CallbackInteractableObject : MonoBehaviour, IInteractable, IInjectable
    {
        [SerializeField] private CanvasGroup _canInteractIndicator;
        [SerializeField] private CallbackObject _callback;

        [Inject] private InteractService _interactService;
        [Inject] private IObjectResolver _resolver;
        
        private bool _opened;

        public void Interact()
        {
            _callback = _resolver.Instantiate(_callback);
            _callback.Callback();
        }

        private void Update()
        {
            if (!_opened && ReferenceEquals(_interactService.CurrentInteractable, this))
                _canInteractIndicator.alpha = 1;
            else
                _canInteractIndicator.alpha = 0;
        }
    }
}