using System;
using App.Gameplay.Systems;
using App.UI.Core;
using App.UI.StartFightPopUp;
using VContainer.Unity;

namespace App.Gameplay.Triggers.StartFight
{
    public class StartFightHandler : IInitializable, IDisposable
    {
        private readonly TriggerEventBus _eventBus;
        private readonly UIFactory  _uiFactory;
        private readonly CombatSystem _combatSystem;
        private StartFightPopUp _popUp;
        
        public StartFightHandler(TriggerEventBus eventBus, UIFactory uiFactory, CombatSystem combatSystem)
        {
            _eventBus = eventBus;
            _uiFactory = uiFactory;
            _combatSystem = combatSystem;
        }

        public void Initialize()
        {
            _eventBus.Subscribe<OnPlayerEnterStartFightTriggerEvent>(OnPlayerEnter);
            _eventBus.Subscribe<OnPlayerExitStartFightTriggerEvent>(OnPlayerExit);
        }

        private void OnPlayerEnter(OnPlayerEnterStartFightTriggerEvent obj)
        {
            if(_combatSystem.IsFighting) return;
            
            if (_popUp == null)
                _popUp = _uiFactory.CreateWindow<StartFightPopUp>();

            if (_popUp.IsOpened == false)
                _popUp.Open();

            _popUp.OnClick.AddListener(OnFightButtonClickHandler);
        }

        private void OnFightButtonClickHandler()
        {
            _combatSystem.StartFight();
            _popUp.CloseAnim();
        }

        private void OnPlayerExit(OnPlayerExitStartFightTriggerEvent obj)
        {
            if(_popUp != null)
                _popUp.CloseAnim();
        }

        public void Dispose()
        {
            if(_popUp != null)
                _popUp.CloseAnim();
            
            _eventBus.Unsubscribe<OnPlayerEnterStartFightTriggerEvent>(OnPlayerEnter);
            _eventBus.Unsubscribe<OnPlayerExitStartFightTriggerEvent>(OnPlayerExit);
        }
    }
}