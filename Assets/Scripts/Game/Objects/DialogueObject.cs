using System.Linq;
using Game.Data;
using Game.Services;
using Scopes;
using UnityEngine;
using VContainer;

namespace Game.Objects
{
    public class DialogueObject : MonoBehaviour, IInteractable, IInjectable
    {
        [SerializeField] private CanvasGroup _canInteractIndicator;
        [SerializeField] private string _key;
        [SerializeField] private bool _isOneTime;
        [SerializeField] private Dialogue _customNoDialogueDialogue;

        [Inject] private DialoguesManager _dialoguesManager;
        [Inject] private InteractService _interactService;

        private bool _interacted;

        public void Interact()
        {
            if (_dialoguesManager.IsDialogueCooldown()) return;
            if (_interacted && _isOneTime) return;

            Debug.Log("Interacting with dialogue object. Dialogues count: " + _dialoguesManager.Dialogues[_key].Count);
            if (_dialoguesManager.Dialogues[_key].Count > 0)
            {
                var dialogue = _dialoguesManager.Dialogues[_key].First();
                _dialoguesManager.StartDialogue(dialogue);
                _dialoguesManager.Dialogues[_key].Remove(dialogue);
                _interacted = true;
            }
            else
            {
                if (_customNoDialogueDialogue != null)
                {
                    _dialoguesManager.StartDialogue(_customNoDialogueDialogue);
                }
                else
                {
                    _dialoguesManager.StartDialogue(_dialoguesManager.NoDialogueDialogue);
                }
            }
        }

        public void Update()
        {
            if (_interacted && _isOneTime)
            {
                _canInteractIndicator.alpha = 0;
            }
            else
            {
                _canInteractIndicator.alpha = ReferenceEquals(_interactService.CurrentInteractable, this) ? 1 : 0;
            }
        }
    }
}