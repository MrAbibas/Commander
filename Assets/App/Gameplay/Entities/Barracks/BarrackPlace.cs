using App.Gameplay.Entities.Currencies;
using UnityEngine;
using UnityEngine.Events;

namespace App.Gameplay.Entities.Barracks
{
    public class BarrackPlace : MonoBehaviour
    {
        public UnityEvent<BarrackPlace> onCurrencyAdded = new();
        [SerializeField]
        private CurrencyStackForBuy currencyStackForBuy;
        public Barrack Barrack { get; private set; }
        public Currency Price { get; private set; }

        private void Start()
        {
            currencyStackForBuy.onCurrencyAdded.AddListener(OnCurrencyAdded);
            currencyStackForBuy.SetTargetCount(Price.Count);
        }

        private void OnDestroy()
        {
            currencyStackForBuy.onCurrencyAdded.RemoveListener(OnCurrencyAdded);
        }

        private void OnCurrencyAdded()
        {
            onCurrencyAdded?.Invoke(this);
        }
    }
}