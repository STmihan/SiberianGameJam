using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.CallbackObjects;
using Game.Data;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Services
{
    public class DialoguesManager : ITickable
    {
        public const float InteractionCooldown = 1.5f;
        private const string DialoguePath = "Data/Dialogues/";

        public Dictionary<string, List<Dialogue>> Dialogues { get; } = new();
        public Dialogue NoDialogueDialogue { get; private set; }

        private readonly InputManager _inputManager;
        private readonly DialogueUI _dialogueUI;
        private readonly FilmModeUI _filmModeUI;
        private readonly IObjectResolver _resolver;

        private float _currentCooldown;

        [Inject]
        public DialoguesManager(InputManager inputManager, DialogueUI dialogueUI, FilmModeUI filmModeUI, IObjectResolver resolver)
        {
            _inputManager = inputManager;
            _dialogueUI = dialogueUI;
            _filmModeUI = filmModeUI;
            _resolver = resolver;
            NoDialogueDialogue = LoadDialogue("NoDialogue");
        }

        public void StartDialogue(string key)
        {
            Dialogue d = LoadDialogue(key);
            StartDialogue(d);
        }
        
        public void StartDialogue(Dialogue dialogue)
        {
            DOTween.Sequence()
                .AppendCallback(() => _filmModeUI.Enable())
                .AppendCallback(() => _inputManager.PlayerInputBlocked = true)
                .AppendCallback(() => Debug.Log(dialogue.ToString()))
                .AppendInterval(FilmModeUI.FadeDuration)
                .AppendCallback(() => _dialogueUI.ShowDialogue(dialogue))
                .AppendCallback(() => _dialogueUI.OnDialogueEnd += OnDialogueEndCallback);
        }

        private void OnDialogueEndCallback(Dialogue dialogue)
        {
            _dialogueUI.OnDialogueEnd -= OnDialogueEndCallback;
            _inputManager.PlayerInputBlocked = false;
            _filmModeUI.Disable();
            _currentCooldown = InteractionCooldown;
            
            if (dialogue.CallbackObject != null)
            {
                CallbackObject callbackObject = _resolver.Instantiate(dialogue.CallbackObject);
                callbackObject.Callback();
            }
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

        public void LoadDialogue(string npc, string dialogue)
        {
            Dialogue loadDialogue = LoadDialogue(dialogue);
            IncludeDialogue(loadDialogue, npc);
        }

        public void RemoveDialogue(string npc, string dialogue)
        {
            if (Dialogues.TryGetValue(npc, out var dialogues))
            {
                dialogues.RemoveAll(d => d.name == dialogue);
            }
        }

        private Dialogue LoadDialogue(string dialogue)
        {
            var load = Resources.Load<Dialogue>(DialoguePath + dialogue);
            if (load == null)
            {
                Debug.LogError($"Dialogue {dialogue} not found.");
                return null;
            }

            return load;
        }

        private void IncludeDialogue(Dialogue dialogue, string npc)
        {
            if (!Dialogues.ContainsKey(npc))
            {
                Dialogues[npc] = new List<Dialogue>();
            }
            Dialogues[npc].Add(dialogue);
        }
    }
}