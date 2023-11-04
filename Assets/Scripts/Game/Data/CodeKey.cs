using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Create CodeKey", fileName = "CodeKey", order = 0)]
    public class CodeKey : ScriptableObject
    {
        [field: SerializeField]
        public string Code { get; private set; }
    }
}