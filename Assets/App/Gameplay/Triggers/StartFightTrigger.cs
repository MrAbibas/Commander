using App.Gameplay.Entities.Characters.Players;
using UnityEngine;

namespace App.Gameplay.Triggers
{
    public class StartFightTrigger : MonoBehaviour
    {
        private PlayerTriggerEventBus _eventBus;
        
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
