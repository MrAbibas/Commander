using System.Collections.Generic;
using App.Factories;
using App.Gameplay.Entities;
using App.Gameplay.Entities.Barracks;
using App.Gameplay.GameplayStates;
using App.Gameplay.GameplayStates.States;
using App.Gameplay.Level;
using App.Gameplay.Providers;
using App.Gameplay.Systems;
using App.Gameplay.Triggers;
using App.Gameplay.Triggers.StartFight;
using App.UI;
using App.UI.Core;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameplayLifetimeScope: LifetimeScope
    {
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
        [SerializeField] private List<BarrackPlace> _barracks;
        [SerializeField] private Formation _friendlyFormation;
        [SerializeField] private List<Formation> _enemiesFormations;
        [SerializeField] private CinemachineCamera _camera;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerSpawnPoint);
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(_friendlyFormation);
            builder.RegisterInstance(_enemiesFormations);
            builder.RegisterInstance(_barracks);
            builder.Register<LoadLevelGameplayState>(Lifetime.Singleton);
            builder.Register<MainLoopGameplayState>(Lifetime.Singleton);
            builder.Register<PauseGameplayState>(Lifetime.Singleton);
            builder.Register<GameplayStateFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<GameplayStateMachine>().AsSelf();
            
            builder.Register<EntityFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerInputSystem>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<BuildingFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<BarrackSystem>().AsSelf();
            builder.RegisterEntryPoint<CombatSystem>().AsSelf();
            
            builder.Register<UIAssetsProvider>(Lifetime.Singleton);
            builder.Register<UIFactory>(Lifetime.Singleton);

            builder.RegisterEntryPoint<TriggerEventBus>().AsSelf();
            builder.RegisterEntryPoint<StartFightHandler>().AsSelf();
        }
    }
}