using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    public class CodeKeyUI : MonoBehaviour
    {
        public event Action<string> OnCodeEntered;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _submitButton;
        [SerializeField] private List<CodeKeyUIItem> _items;

        [Inject] private InputManager _inputManager;
        
        private void Start()
        {
            _submitButton.onClick.AddListener(Submit);
            Disable(true);
        }

        public void Enable()
        {
            _canvasGroup.DOFade(1, 0.3f);
            foreach (CodeKeyUIItem item in _items)
            {
                item.Index = 0;
            }
            _inputManager.PlayerInputBlocked = true;
        }
        
        public void Disable(bool immediate = false)
        {
            _canvasGroup.DOFade(0, immediate ? 0 : 0.3f);
            _inputManager.PlayerInputBlocked = false;
        }

        private void Update()
        {
            if (!_inputManager.PlayerInputBlocked) return;
            if (_inputManager.GetBackInput())
            {
                Disable();
            }
        }

        private string GetCode()
        {
            var code = "";
            foreach (var item in _items)
            {
                code += item.Index.ToString();
            }

            return code;
        }

        private void Submit()
        {
            OnCodeEntered?.Invoke(GetCode());
        }
    }
}