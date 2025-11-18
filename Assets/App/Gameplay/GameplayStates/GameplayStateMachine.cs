using App.Core.FSM;
using App.Factories;
using App.Gameplay.GameplayStates.States;
using VContainer.Unity;

namespace App.Gameplay.GameplayStates
{
    public class GameplayStateMachine: StateMachine, IInitializable ,ITickable
    {
        private readonly IGameplayStateFactory _stateFactory;
        public bool LevelLoaded { get; set; }
        public bool Paused { get; set; }

        public GameplayStateMachine(IGameplayStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Initialize()
        {
            var loadLevelState = _stateFactory.Create<LoadLevelGameplayState>();
            var mainLoopState = _stateFactory.Create<MainLoopGameplayState>();
            var pauseState = _stateFactory.Create<PauseGameplayState>();
            
            AddTransition(loadLevelState, mainLoopState, new FuncPredicate(() => LevelLoaded));
            AddTransition(mainLoopState, pauseState, new FuncPredicate(() => Paused));
            AddTransition(pauseState, mainLoopState, new FuncPredicate(() => Paused == false));
            SetState(loadLevelState);
        }

        public void Tick()
        {
            Update();
        }
    }
}