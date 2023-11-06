using System.Linq;
using Game.CallbackObjects;
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

        [Inject] private DialoguesManager _dialoguesManager;
        [Inject] private InteractService _interactService;

        public void Interact()
        {
            if (_dialoguesManager.IsDialogueCooldown()) return;

            Debug.Log("Interacting with dialogue object. Dialogues count: " + _dialoguesManager.Dialogues[_key].Count);
            if (_dialoguesManager.Dialogues[_key].Count > 0)
            {
                var dialogue = _dialoguesManager.Dialogues[_key].First();
                _dialoguesManager.StartDialogue(dialogue);
                _dialoguesManager.Dialogues[_key].Remove(dialogue);
            }
            else
            {
                _dialoguesManager.StartDialogue(_dialoguesManager.NoDialogueDialogue);
            }
        }

        public void Update()
        {
            _canInteractIndicator.alpha = ReferenceEquals(_interactService.CurrentInteractable, this) ? 1 : 0;
        }
    }
}