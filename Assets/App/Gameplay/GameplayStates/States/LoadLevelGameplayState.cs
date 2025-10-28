namespace App.Gameplay.GameplayStates.States
{
    public class LoadLevelGameplayState : IGameplayState
    {
        private readonly GameplayStateMachine _stateMachine;

        public LoadLevelGameplayState(GameplayStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.LevelLoaded = true;
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}