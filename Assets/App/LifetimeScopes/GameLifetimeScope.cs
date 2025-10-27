using App.Factory;
using App.GameStates;
using App.GameStates.States;
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
            builder.RegisterEntryPoint<GameStateMachine>();
        }
    }
}
