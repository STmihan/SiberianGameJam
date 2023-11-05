using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class DZZone : MonoBehaviour
    {
        
        [SerializeField] private float _radius;
        [SerializeField] private Collider2D _collider;
        
        private void Start()
        {
            _collider = GetComponent<Collider2D>();
        }

        public bool IsInZone(float radius, Vector2 pos)
        {
            _radius = radius;
            Vector2 center = _collider.bounds.center;
            Vector2 size = _collider.bounds.size;
            size -= new Vector2(_radius * 2, _radius * 2);
            if (IsInBounds(new Bounds(center, size), pos)) return true;
            
            return false;
        }
        
        private bool IsInBounds(Bounds bounds, Vector2 pos)
        {
            return bounds.Contains(pos);
        }
    }
}