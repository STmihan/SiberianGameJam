using System.Collections.Generic;
using UnityEngine;

namespace Game.DialogueObjects
{
    [CreateAssetMenu(menuName = "Game/Create Dialogue", fileName = "Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public bool IsFinished { get; set; } = false;

        [field: SerializeField]
        [field: Multiline]
        public List<string> Text { get; private set; } = new();

        public override string ToString()
        {
            string result = $"{name}\n";
            foreach (var s in Text)
            {
                result += s;
                result += "\n";
            }

            return result;
        }
    }
}