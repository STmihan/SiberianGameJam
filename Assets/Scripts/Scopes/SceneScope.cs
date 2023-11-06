using System;
using System.Linq;
using Game;
using Game.Factories;
using Game.Objects;
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
        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private CodeKeyUI _codeKeyUI;
        [SerializeField] private LoadingUI _loadingUI;
        [SerializeField] private NotesUI _notesUI;
        [Space]
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private DevilZoneController _devilZoneController;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingUI);
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameController>();
            
            builder.RegisterInstance(_devilZoneController);
            builder.Register<InteractService>(Lifetime.Singleton);
            builder.Register<PlayerControllerFactory>(Lifetime.Singleton);

            builder.RegisterInstance(_codeKeyUI);
            builder.RegisterInstance(_inventoryUI);
            builder.RegisterInstance(_cameraController);
            builder.RegisterInstance(_dialogueUI);
            builder.RegisterInstance(_filmModeUI);
            builder.RegisterInstance(_notesUI);

            builder.Register<DialoguesManager>(Lifetime.Singleton);
        }

        private void Update()
        {
            Container.Resolve<DialoguesManager>().Tick();
        }
    }
}