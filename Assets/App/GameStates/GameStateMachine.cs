using App.Core.FSM;
using App.Factories;
using App.GameStates.States;
using VContainer.Unity;

namespace App.GameStates
{
    public class GameStateMachine : StateMachine, IInitializable ,ITickable
    {
        public bool GameplaySceneLoaded { get; set; } = false;
        private readonly IGameStateFactory _stateFactory;

        public GameStateMachine(IGameStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Initialize()
        {
            var bootstrapState = _stateFactory.Create<BootstrapGameState>();
            var gameplayState = _stateFactory.Create<GameplayState>();
            AddTransition(bootstrapState, gameplayState, new FuncPredicate(() => GameplaySceneLoaded));
            SetState(bootstrapState);
        }

        public void Tick()
        {
            Update();
        }
    }
}