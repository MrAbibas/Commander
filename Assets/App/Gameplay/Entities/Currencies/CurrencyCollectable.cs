using UnityEngine;
using UnityEngine.Events;

namespace App.Gameplay.Entities.Currencies
{
    public class CurrencyCollectable : MonoBehaviour
    {
        public UnityEvent<CurrencyCollectable> onDespawn;
        [field: SerializeField] public Currency Currency { get; private set; }

        public void Despawn()
        {
            onDespawn?.Invoke(this);
        }
    }
}