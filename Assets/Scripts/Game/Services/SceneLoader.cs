using DG.Tweening;
using Game.Data;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Services
{
    public class SceneLoader
    {
        private GameObject _currentScene;
        
        private readonly SceneList _sceneList;
        private readonly IObjectResolver _container;
        private readonly LoadingUI _ui;
        
        [Inject]
        public SceneLoader(IObjectResolver container, LoadingUI ui)
        {
            _container = container;
            _ui = ui;
            _sceneList = SceneList.Get();
        }
        
        public void LoadScene(string sceneName)
        {
            DOTween.Sequence()
                .Append(_ui.Fade(true))
                .AppendCallback(() => CreateScene(sceneName))
                .Append(_ui.Fade(false));
        }

        private void CreateScene(string sceneName)
        {
            var go = _sceneList.GetScene(sceneName);
            if (go == null)
            {
                Debug.LogError($"Scene {sceneName} not found");
            }
            
            if (_currentScene != null)
            {
                Object.Destroy(_currentScene);
            }
            
            _currentScene = _container.Instantiate(go);
            
        }
    }
}