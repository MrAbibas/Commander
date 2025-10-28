using App.Factory;
using App.Gameplay.Entities;
using App.Gameplay.Entities.Players;
using App.Gameplay.Providers;
using Unity.Cinemachine;

namespace App.Gameplay.GameplayStates.States
{
    public class LoadLevelGameplayState : IGameplayState
    {
        private readonly GameplayStateMachine _stateMachine;
        private readonly IPlayerProvider _playerProvider;
        private readonly PlayerSpawnPoint _playerSpawnPoint;
        private readonly IEntityFactory _entityFactory;
        private readonly CinemachineCamera _camera;

        public LoadLevelGameplayState(GameplayStateMachine stateMachine,
            IPlayerProvider playerProvider,
            PlayerSpawnPoint playerSpawnPoint,
            IEntityFactory  entityFactory,
            CinemachineCamera camera)
        {
            _stateMachine = stateMachine;
            _playerProvider = playerProvider;
            _playerSpawnPoint = playerSpawnPoint;
            _entityFactory = entityFactory;
            _camera = camera;
        }

        public void Enter()
        {
            _stateMachine.LevelLoaded = true;
            var player = _entityFactory.Create<Player>(EntityId.Player);
            player.transform.position = _playerSpawnPoint.transform.position;
            _playerProvider.SetPlayer(player);
            _camera.Target.TrackingTarget = player.transform;
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}