using DG.Tweening;
using Game.DialogueObjects;
using Game.UI;
using UnityEngine;
using VContainer;

namespace Game.Services
{
    public class DialoguesManager
    {
        private readonly InputManager _inputManager;
        private readonly DialogueUI _ui;

        [Inject]
        public DialoguesManager(InputManager inputManager, DialogueUI ui)
        {
            _inputManager = inputManager;
            _ui = ui;
        }

        public void StartDialogue(Dialogue dialogue)
        {
            DOTween.Sequence()
                .AppendCallback(() => FilmModeUI.Instance.Enable())
                .AppendCallback(() => _inputManager.PlayerInputBlocked = true)
                .AppendCallback(() => Debug.Log(dialogue.ToString()))
                .AppendInterval(FilmModeUI.FadeDuration)
                .AppendCallback(() => _ui.ShowDialogue(dialogue))
                .AppendCallback(() => _ui.OnDialogueEnd += OnDialogueEnd);
        }

        private void OnDialogueEnd(Dialogue dialogue)
        {
            _ui.OnDialogueEnd -= OnDialogueEnd;
            _inputManager.PlayerInputBlocked = false;
            FilmModeUI.Instance.Disable();
        }
    }
}