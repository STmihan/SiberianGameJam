using System;
using Game.Factories;
using UnityEngine;
using VContainer;

namespace Game
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [Inject] private PlayerControllerFactory _playerControllerFactory;
        [Inject] private CameraController _cameraController;
        
        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            PlayerController playerController = _playerControllerFactory.Create(transform.position);
            _cameraController.SetTarget(playerController.transform);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.5f);
            Gizmos.color = Color.white;
        }
#endif
    }
}