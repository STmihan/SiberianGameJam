using System.Collections.Generic;
using Game.Data;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class NotesUI : MonoBehaviour, IUI
    {
        private const string NotePath = "Data/Notes/";
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _titleText;
        
        private readonly List<Note> _notes = new();

        private void Start()
        {
            Toggle(false);
        }

        public void Toggle(bool toggle)
        {
            _canvasGroup.alpha = toggle ? 1 : 0;
            _canvasGroup.blocksRaycasts = toggle;
        }
        
        public void LoadNote(string key)
        {
            Note note = Resources.Load<Note>(NotePath + key);
            string text = note.Title + "\n\n" + note.NoteText + "\n\n";
            _titleText.text = text;
        }
    }
}