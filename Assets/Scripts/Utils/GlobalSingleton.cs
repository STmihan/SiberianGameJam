using System;
using UnityEngine;

namespace Utils
{
    public abstract class GlobalSingleton<T> : MonoBehaviour
    {
        public static T Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning($"There is already an instance of {typeof(T)} in the scene.");
                Destroy(this);
            }

            Instance = (T) Convert.ChangeType(this, typeof(T));
            DontDestroyOnLoad(this);
        }
    }
}