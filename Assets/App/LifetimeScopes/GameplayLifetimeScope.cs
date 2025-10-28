using App.Factory;
using App.Gameplay.GameplayStates;
using App.Gameplay.GameplayStates.States;
using App.Gameplay.Providers;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameplayLifetimeScope: LifetimeScope
    {
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
        [SerializeField] private CinemachineCamera _camera;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerSpawnPoint);
            builder.RegisterInstance(_camera);
            builder.Register<LoadLevelGameplayState>(Lifetime.Singleton);
            builder.Register<MainLoopGameplayState>(Lifetime.Singleton);
            builder.Register<PauseGameplayState>(Lifetime.Singleton);
            builder.Register<GameplayStateFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<GameplayStateMachine>();
            
            builder.Register<EntityFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}