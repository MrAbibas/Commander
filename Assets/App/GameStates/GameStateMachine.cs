using App.Factory;
using App.GameStates.States;
using Core.FSM;
using VContainer.Unity;

namespace App.GameStates
{
    public class GameStateMachine : StateMachine, IInitializable ,ITickable
    {
        public bool GameplaySceneLoaded { get; set; } = false;
        private readonly GameStateFactory _stateFactory;

        public GameStateMachine(GameStateFactory stateFactory)
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