using System.Collections.Generic;
using System.Linq;
using Game.Services;
using Game.UI;
using UnityEngine;
using VContainer;

namespace Game.DialogueObjects
{
    public class DialogueObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private CanvasGroup _canInteractIndicator;
        [SerializeField] private List<Dialogue> _startDialogues;
        [SerializeField] private Dialogue _noDialogueDialogue;

        private readonly List<Dialogue> _dialogues = new();

        [Inject] private DialoguesManager _dialoguesManager;

        public const float InteractionCooldown = 2f;

        private float _currentCooldown;
        
        private void Awake()
        {
            _dialogues.AddRange(_startDialogues);
        }

        public void Interact()
        {
            if (_currentCooldown > 0) return;
            
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

            _currentCooldown = InteractionCooldown;
        }

        public void Update()
        {
            _canInteractIndicator.alpha = ReferenceEquals(PlayerController.Instance.CurrentInteractable, this) ? 1 : 0;
            ProcessTimer();
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

        private void ProcessTimer()
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
                if (_currentCooldown < 0) _currentCooldown = 0;
            }
        }
    }
}