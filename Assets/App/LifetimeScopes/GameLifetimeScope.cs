using App.Factories;
using App.GameStates;
using App.GameStates.States;
using App.Services;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<BootstrapGameState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);
            builder.Register<GameStateFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<GameStateMachine>().AsSelf();
            builder.Register<ConfigurationService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
