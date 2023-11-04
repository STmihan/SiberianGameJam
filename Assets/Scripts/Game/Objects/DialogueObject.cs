using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Services;
using UnityEngine;
using VContainer;

namespace Game.Objects
{
    public class DialogueObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private CanvasGroup _canInteractIndicator;
        [SerializeField] private List<Dialogue> _startDialogues;
        [SerializeField] private Dialogue _noDialogueDialogue;

        private readonly List<Dialogue> _dialogues = new();

        [Inject] private DialoguesManager _dialoguesManager;
        [Inject] private InteractService _interactService;

        private void Awake()
        {
            _dialogues.AddRange(_startDialogues);
        }

        public void Interact()
        {
            if (_dialoguesManager.IsDialogueCooldown()) return;

            Debug.Log("Interacting with dialogue object. Dialogues count: " + _dialogues.Count);
            if (_dialogues.Count > 0)
            {
                var dialogue = _dialogues.First();
                _dialoguesManager.StartDialogue(dialogue);
                _dialogues.Remove(dialogue);
            }
            else
            {
                _dialoguesManager.StartDialogue(_noDialogueDialogue);
            }
        }

        public void Update()
        {
            _canInteractIndicator.alpha = ReferenceEquals(_interactService.CurrentInteractable, this) ? 1 : 0;
        }

        public void LoadDialogue(string dialogue)
        {
            var load = Resources.Load<Dialogue>(dialogue);
            if (load == null)
            {
                Debug.LogError($"Dialogue {dialogue} not found.");
                return;
            }

            _dialogues.Add(load);
        }
    }
}