using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game.Data;
using Game.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.UI
{
    public class DialogueUI : MonoBehaviour
    {
        public event Action<Dialogue> OnDialogueEnd;

        public const float TextSpeed = 0.5f;
        public const float PanelTransitionSpeed = 1f;
        
        [SerializeField] private Image _panel;
        [SerializeField] private TMP_Text _text;

        [Inject]
        private InputManager _inputManager;
        
        private bool _typing;
        private Dialogue _currentDialogue;
        private int _currentTextIndex;
        private TweenerCore<string, string, StringOptions> _typingTween;

        private float _startPanelPosY;
        private float _panelHeight;
        private void Start()
        {
            _startPanelPosY = _panel.rectTransform.anchoredPosition.y;
            _panelHeight = _panel.rectTransform.rect.height;
            _panel.rectTransform.anchoredPosition = new Vector2(_panel.rectTransform.anchoredPosition.x, -_panelHeight);
        }

        public void ShowDialogue(Dialogue dialogue)
        {
            _panel.rectTransform.DOAnchorPosY(_startPanelPosY, TextSpeed);
            _currentDialogue = dialogue;
            _currentTextIndex = 0;
            TypeNextText(dialogue.Text[_currentTextIndex]);
        }

        private void TypeNextText(string text)
        {
            _text.text = string.Empty;
            string currentText = string.Empty;
            _typing = true;
            _typingTween = DOTween.To(() => currentText, x => currentText = x, text, PanelTransitionSpeed).OnUpdate(() => _text.text = currentText)
                .OnComplete(() => _typing = false);
        }

        private void NextText()
        {
            _currentTextIndex++;
            if (_currentTextIndex >= _currentDialogue.Text.Count)
            {
                OnDialogueEnd?.Invoke(_currentDialogue);
                _currentDialogue = null;
                _panel.rectTransform.DOAnchorPosY(-_panelHeight, TextSpeed);
            }
            else
            {
                TypeNextText(_currentDialogue.Text[_currentTextIndex]);
            }
        }

        private void Update()
        {
            if (_currentDialogue == null) return;
            if (_inputManager.GetDialogueInput())
            {
                if (_typing)
                {
                    _typingTween.Complete(true);
                }
                else
                {
                    NextText();
                }
            }
        }
    }
}