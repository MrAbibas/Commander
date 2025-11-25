using App.Gameplay.Entities.Characters.Players;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Triggers.StartFight
{
    public class StartFightTrigger : MonoBehaviour
    {
        private TriggerEventBus _eventBus;

        [Inject]
        public void Construct(TriggerEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player player))
                _eventBus.Publish(new OnPlayerEnterStartFightTriggerEvent());        
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                _eventBus.Publish(new OnPlayerExitStartFightTriggerEvent());
        }
    }
}
