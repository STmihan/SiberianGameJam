﻿using System.Collections.Generic;
using DG.Tweening;
using Game.Data;
using Game.Factories;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Services
{
    public class SceneLoader
    {
        private GameObject _currentScene;

        private readonly IObjectResolver _container;
        private readonly SceneList _sceneList;
        private readonly LoadingUI _ui;
        private readonly InputManager _inputManager;

        private Dictionary<string, GameObject> _scenes = new();

        [Inject]
        public SceneLoader(IObjectResolver container, LoadingUI ui, InputManager inputManager)
        {
            _container = container;
            _ui = ui;
            _inputManager = inputManager;
            _sceneList = SceneList.Get();
        }

        public void LoadScene(string sceneName, string spawnPointKey = "")
        {
            DOTween.Sequence()
                .AppendCallback(() => _inputManager.PlayerInputBlocked = true)
                .Append(_ui.Fade(true))
                .AppendCallback(() => CreateScene(sceneName))
                .AppendCallback(() => SelectSpawnPoint(spawnPointKey))
                .Append(_ui.Fade(false))
                .AppendCallback(() => _inputManager.PlayerInputBlocked = false);
        }

        private void SelectSpawnPoint(string spawnPointKey)
        {
            var spawnPoints = _currentScene.GetComponentsInChildren<PlayerSpawnPoint>();
            if (spawnPoints.Length == 0)
            {
                Debug.LogError("No spawn points found");
                return;
            }

            if (spawnPointKey == string.Empty)
            {
                spawnPoints[0].Spawn();
            }
            else
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    if (spawnPoint.Key == spawnPointKey)
                    {
                        spawnPoint.Spawn();
                    }
                }
            }
        }

        private void CreateScene(string sceneName)
        {
            if (_currentScene != null)
            {
                _currentScene.SetActive(false);
            }
            
            if (!_scenes.ContainsKey(sceneName))
            {
                var scene = _sceneList.GetScene(sceneName);
                _currentScene = _container.Instantiate(scene);
                _scenes[sceneName] = _currentScene;
            }
            else
            {
                _currentScene = _scenes[sceneName];
                _currentScene.SetActive(true);
            }
            
            var go = _sceneList.GetScene(sceneName);
            if (go == null)
            {
                Debug.LogError($"Scene {sceneName} not found");
            }
        }
    }
}