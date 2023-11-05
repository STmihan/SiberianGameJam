using UnityEngine;

namespace Game.Services
{
    public class DevilZoneController : MonoBehaviour
    {
        private static readonly int CircleRadius = Shader.PropertyToID("_CircleRadius");
        private static readonly int CirclePos = Shader.PropertyToID("_CirclePos");
        private const string MatPath = "Materials/DZ_Mat";

        [SerializeField] private float _maxRadius = 1.2f;

        public bool Enabled { get; private set; } = true;
        public DZZone Zone { get; set; }

        public Material Mat
        {
            get
            {
                if (_material == null)
                {
                    var material = Resources.Load<Material>(MatPath);
                    var copy = new Material(material);
                    _material = copy;
                }

                return _material;
            }
        }

        private float _radius;
        private Material _material;

        public bool UpdateDZ(bool toggle, Vector2 pos)
        {
            if (Zone == null) toggle = false;
            if (Zone != null && !Zone.IsInZone(_maxRadius, pos)) toggle = false;
            Enabled = toggle;
            Mat.SetVector(CirclePos, pos);
            Mat.SetFloat(CircleRadius, _radius);
            _radius = Mathf.Lerp(_radius, Enabled ? _maxRadius : 0, Time.deltaTime * 5);

            return Enabled;
        }
    }
}