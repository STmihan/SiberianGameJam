using DG.Tweening;
using Game.DialogueObjects;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Services
{
    public class DialoguesManager : ITickable
    {
        private readonly InputManager _inputManager;
        private readonly DialogueUI _dialogueUI;
        private readonly FilmModeUI _filmModeUI;

        public const float InteractionCooldown = 1.5f;

        private float _currentCooldown;

        [Inject]
        public DialoguesManager(InputManager inputManager, DialogueUI dialogueUI, FilmModeUI filmModeUI)
        {
            _inputManager = inputManager;
            _dialogueUI = dialogueUI;
            _filmModeUI = filmModeUI;
        }

        public void StartDialogue(Dialogue dialogue)
        {
            DOTween.Sequence()
                .AppendCallback(() => _filmModeUI.Enable())
                .AppendCallback(() => _inputManager.PlayerInputBlocked = true)
                .AppendCallback(() => Debug.Log(dialogue.ToString()))
                .AppendInterval(FilmModeUI.FadeDuration)
                .AppendCallback(() => _dialogueUI.ShowDialogue(dialogue))
                .AppendCallback(() => _dialogueUI.OnDialogueEnd += OnDialogueEnd);
        }

        private void OnDialogueEnd(Dialogue dialogue)
        {
            _dialogueUI.OnDialogueEnd -= OnDialogueEnd;
            _inputManager.PlayerInputBlocked = false;
            _filmModeUI.Disable();
            _currentCooldown = InteractionCooldown;
        }

        public bool IsDialogueCooldown()
        {
            return _currentCooldown > 0;
        }

        private void ProcessTimer()
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
                if (_currentCooldown < 0) _currentCooldown = 0;
            }
        }

        public void Tick()
        {
            ProcessTimer();
        }
    }
}