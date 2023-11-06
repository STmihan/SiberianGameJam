using System;
using Game.Services;
using Scopes;
using UnityEngine;
using VContainer;

namespace Game.Objects
{
    public class GoToSceneObject : MonoBehaviour, IInjectable
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private string _spawnPointKey;
        [Inject] private SceneLoader _sceneLoader;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>())
            {
                _sceneLoader.LoadScene(_sceneName, _spawnPointKey);
            }
        }
    }
}