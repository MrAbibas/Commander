using App.Gameplay.Entities.Currencies;
using UnityEngine;
using UnityEngine.Events;

namespace App.Gameplay.Entities.Barracks
{
    public class BarrackPlace : MonoBehaviour
    {
        public UnityEvent onCurrencyAdded = new();
        public Barrack Barrack { get; private set; }
        public Currency Currency { get; private set; }

        public void AddCurrency(Currency currency)
        {
            Currency += currency;
            onCurrencyAdded?.Invoke();
        }
    }
}