using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Factories
{
    public class PlayerControllerFactory
    {
        private const string PlayerPrefabPath = "Prefabs/Player";
        private readonly IObjectResolver _resolver;
        private readonly PlayerController _prefab;
        
        public PlayerControllerFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
            _prefab = Resources.Load<PlayerController>(PlayerPrefabPath);
        }


        public PlayerController Create(Vector3 pos)
        {
            return _resolver.Instantiate(_prefab, pos, Quaternion.identity);
        }
    }
}