using Game.Services;
using Scopes;
using UnityEngine;
using VContainer;

namespace Game.Objects
{
    public class DevilZoneObject : MonoBehaviour, IInjectable
    {
        private const string MatPath = "Materials/DZ_Mat";

        [Inject] private DevilZoneController _devilZoneController;

        private Collider2D _collider;

        private void Start()
        {
            var material = Resources.Load<Material>(MatPath);
            GetComponent<Renderer>().material = material;
            if (TryGetComponent(out Collider2D col)) _collider = col;
        }

        private void Update()
        {
            if (_collider != null && !_collider.isTrigger)
            {
                _collider.enabled = _devilZoneController.Enabled;
            }
        }
    }
}