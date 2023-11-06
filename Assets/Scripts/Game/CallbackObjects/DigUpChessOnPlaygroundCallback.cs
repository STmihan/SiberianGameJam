using Scopes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.CallbackObjects
{
    public class DigUpChessOnPlaygroundCallback : CallbackObject, IInjectable
    {
        [SerializeField] private GameObject _chessPrefab;
        
        [Inject] private IObjectResolver _objectResolver;
        
        public override void Callback(object payload = null)
        {
            _objectResolver.Instantiate(_chessPrefab, transform.position, Quaternion.identity);
            if (TryGetComponent(out Renderer r))
            {
                r.enabled = false;
            }
            if (TryGetComponent(out Collider2D c))
            {
                c.enabled = false;
            }
        }
    }
}