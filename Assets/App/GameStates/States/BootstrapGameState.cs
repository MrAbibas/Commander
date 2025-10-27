using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.GameStates.States
{
    public class BootstrapGameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private AsyncOperation _loadSceneOperation;
        
        public void Enter()
        {
             _loadSceneOperation = SceneManager.LoadSceneAsync("GameplayScene");
        }

        public void Update()
        {
            if (_loadSceneOperation.isDone)
                _stateMachine.GameplaySceneLoaded = true;
        }

        public void Exit()
        {
        }
    }
}