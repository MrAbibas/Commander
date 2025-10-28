using App.Factory;
using App.Gameplay.GameplayStates;
using App.Gameplay.GameplayStates.States;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameplayLifetimeScope: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LoadLevelGameplayState>(Lifetime.Singleton);
            builder.Register<MainLoopGameplayState>(Lifetime.Singleton);
            builder.Register<PauseGameplayState>(Lifetime.Singleton);
            builder.Register<GameplayStateFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<GameplayStateMachine>();
            
            
        }
    }
}