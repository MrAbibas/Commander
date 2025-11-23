using System;
using App.UI.Core;
using App.UI.StartFightPopUp;
using VContainer.Unity;

namespace App.Gameplay.Triggers.StartFight
{
    public class StartFightHandler : IInitializable, IDisposable
    {
        private readonly TriggerEventBus _eventBus;
        private readonly UIFactory  _uiFactory;
        private StartFightPopUp _popUp;
        
        public StartFightHandler(TriggerEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<OnPlayerEnterStartFightTriggerEvent>(OnPlayerEnter);
            _eventBus.Subscribe<OnPlayerExitStartFightTriggerEvent>(OnPlayerExit);
        }

        private void OnPlayerEnter(OnPlayerEnterStartFightTriggerEvent obj)
        {
            if (_popUp == null)
                _popUp = _uiFactory.CreateWindow<StartFightPopUp>();

            if (_popUp.IsOpened == false)
                _popUp.Open();
        }
        
        private void OnPlayerExit(OnPlayerExitStartFightTriggerEvent obj)
        {
            if(_popUp != null)
                _popUp.Close();
        }

        public void Dispose()
        {
            if(_popUp != null)
                _popUp.Close();
            
            _eventBus.Unsubscribe<OnPlayerEnterStartFightTriggerEvent>(OnPlayerEnter);
            _eventBus.Unsubscribe<OnPlayerExitStartFightTriggerEvent>(OnPlayerExit);
        }
    }
}