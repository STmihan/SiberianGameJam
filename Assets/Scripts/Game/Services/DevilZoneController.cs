using System;
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

        private DZZone _zone;

        private float _radius;

        public void UpdateDZ(bool toggle)
        {
            if (_zone == null) toggle = false;
            if (_zone != null && !_zone.IsInZone(_maxRadius, transform.position)) toggle = false;
            Enabled = toggle;
            _dzMaterial.SetVector(CirclePos, transform.position);
            _dzMaterial.SetFloat(CircleRadius, _radius);
            _radius = Mathf.Lerp(_radius, toggle ? _maxRadius : 0, Time.deltaTime * 5);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out DZZone zone))
            {
                _zone = zone;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out DZZone _))
            {
                _zone = null;
            }
        }
    }
}