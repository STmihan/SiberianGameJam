using System.Collections.Generic;
using Game.CallbackObjects;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Create Dialogue", fileName = "Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public bool IsFinished { get; set; } = false;

        [field: SerializeField]
        [field: Multiline]
        public List<string> Text { get; private set; } = new();
        
        [field: SerializeField]
        public CallbackObject CallbackObject { get; private set; }

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