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
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_dialogueUI);
            builder.Register<DialoguesManager>(Lifetime.Singleton);
        }
    }
}