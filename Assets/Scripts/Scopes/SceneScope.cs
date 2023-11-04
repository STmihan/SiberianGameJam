using System;
using Game;
using Game.Factories;
using Game.Services;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class SceneScope : LifetimeScope
    {
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private FilmModeUI _filmModeUI;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private CodeKeyUI _codeKeyUI;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InteractService>(Lifetime.Singleton);
            builder.Register<PlayerControllerFactory>(Lifetime.Singleton);

            builder.RegisterInstance(_codeKeyUI);
            builder.RegisterInstance(_inventoryUI);
            builder.RegisterInstance(_cameraController);
            builder.RegisterInstance(_dialogueUI);
            builder.RegisterInstance(_filmModeUI);

            builder.Register<DialoguesManager>(Lifetime.Singleton);
        }

        private void Update()
        {
            Container.Resolve<DialoguesManager>().Tick();
        }
    }
}