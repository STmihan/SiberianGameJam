using Game.CallbackObjects;
using Game.Data;
using Game.Services;
using Game.UI;
using UnityEngine;
using VContainer;

namespace Game.Objects
{
    public class CodeKeyObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private CodeKey _codeKey;
        [SerializeField] private CanvasGroup _canInteractIndicator;
        [SerializeField] private CallbackObject _onOpened;
        
        [Inject] private InteractService _interactService;
        [Inject] private CodeKeyUI _codeKeyUi;

        private bool _opened;
        
        private const float InteractDelay = 1.5f;
        private float _interactDelayTimer;
        
        public void Interact()
        {
            if (_opened) return;
            if (_interactDelayTimer > 0) return;
            
            _codeKeyUi.OnCodeEntered += OnCodeEntered;
            _codeKeyUi.Enable();
        }

        private void OnCodeEntered(string code)
        {
            _interactDelayTimer = InteractDelay;
            _codeKeyUi.OnCodeEntered -= OnCodeEntered;
            _codeKeyUi.Disable();
            if (_codeKey.Code == code)
            {
                _opened = true;
                Debug.Log("Code key opened. Code: " + code);
                _onOpened.Callback();
            }
        }
        
        private void Update()
        {
            if (!_opened && ReferenceEquals(_interactService.CurrentInteractable, this))
                _canInteractIndicator.alpha = 1;
            else
                _canInteractIndicator.alpha = 0;
            ProcessDelay();
        }

        private void ProcessDelay()
        {
            if (_interactDelayTimer <= 0) return;
            _interactDelayTimer -= Time.deltaTime;
        }
    }
}