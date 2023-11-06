using Game.Services;
using Scopes;
using UnityEngine;
using VContainer;

namespace Game.Objects
{
    public class FindChessTrigger : MonoBehaviour, IInjectable
    {
        [Inject] private DevilZoneController _devilZoneController;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>())
            {
                if (_devilZoneController.Enabled)
                {
                    GameController.SendEvent(GameEvent.FindChessInDungeon);
                }
            }
        }
    }
}