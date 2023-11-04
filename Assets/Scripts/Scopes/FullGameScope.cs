using Game.Services;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class FullGameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputManager>(Lifetime.Singleton);
        }
    }
}