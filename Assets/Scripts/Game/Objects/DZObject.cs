﻿using Game.Services;
using Scopes;
using UnityEngine;
using VContainer;

namespace Game.Objects
{
    public class DZObject : MonoBehaviour, IInjectable
    {
        [Inject] private DevilZoneController _devilZoneController;

        private Collider2D _collider;

        private void Start()
        {
            var componentsInChildren = GetComponentsInChildren<Renderer>();
            foreach (var component in componentsInChildren)
            {
                component.material = _devilZoneController.Mat;
            }
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