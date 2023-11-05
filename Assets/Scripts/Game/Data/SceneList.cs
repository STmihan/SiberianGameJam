using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Create SceneList", fileName = "SceneList")]
    public class SceneList : ScriptableObject
    {
        private const string SceneListPath = "Data/SceneList";
        
        [SerializeField] private List<GameObject> _scenes;
        
        public static SceneList Get()
        {
            return Resources.Load<SceneList>(SceneListPath);
        }

        public GameObject GetScene(string sceneName)
        {
            return _scenes.First(s => s.name == sceneName);
        }
    }
}