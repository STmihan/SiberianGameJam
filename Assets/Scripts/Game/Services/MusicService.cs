using UnityEngine;

namespace Game.Services
{
    public class MusicService : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }
}