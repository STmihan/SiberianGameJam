using Cinemachine;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;

        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetTarget(Transform target)
        {
            _virtualCamera.Follow = target;
            _virtualCamera.LookAt = target;
        }
    }
}