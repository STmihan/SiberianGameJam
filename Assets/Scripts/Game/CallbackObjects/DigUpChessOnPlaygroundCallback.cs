using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.CallbackObjects
{
    public class DigUpChessOnPlaygroundCallback : CallbackObject
    {
        [SerializeField] private GameObject _chessPrefab;
        [Inject] private IObjectResolver _resolver;
        
        public override void Callback(object payload = null)
        {
            if (TryGetComponent(out Renderer r))
            {
                r.enabled = false;
            }
            if (TryGetComponent(out Collider2D c))
            {
                c.enabled = false;
            }
            _resolver.Instantiate(_chessPrefab, transform.position, Quaternion.identity);
            GameController.SendEvent(GameEvent.ChessDigUp);
        }
    }
}