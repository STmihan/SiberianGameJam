using UnityEngine;

namespace Game.Services
{
    public class DevilZoneController : MonoBehaviour
    {
        private static readonly int CircleRadius = Shader.PropertyToID("_CircleRadius");
        private static readonly int CirclePos = Shader.PropertyToID("_CirclePos");

        [SerializeField] private float _maxRadius = 1.2f;
        [SerializeField] private Material _dzMaterial;
        
        public bool Enabled { get; private set; } = true;
        public DZZone Zone { get; set; }


        private float _radius;

        public bool UpdateDZ(bool toggle, Vector2 pos)
        {
            if (Zone == null) toggle = false;
            if (Zone != null && !Zone.IsInZone(_maxRadius, pos)) toggle = false;
            Enabled = toggle;
            _dzMaterial.SetVector(CirclePos, pos);
            _dzMaterial.SetFloat(CircleRadius, _radius);
            _radius = Mathf.Lerp(_radius, Enabled ? _maxRadius : 0, Time.deltaTime * 5);

            return Enabled;
        }
    }
}