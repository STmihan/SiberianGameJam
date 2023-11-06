using UnityEngine;

namespace Game.Data
{
    public class Note : ScriptableObject
    {
        [field: SerializeField]
        public string Key { get; private set; }
        [field: SerializeField]
        public string Title { get; private set; }
        [field: SerializeField]
        [field: TextArea]
        public string NoteText { get; private set; }
    }
}