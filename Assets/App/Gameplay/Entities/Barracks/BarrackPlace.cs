using App.Gameplay.Entities.Currencies;
using UnityEngine;
using UnityEngine.Events;

namespace App.Gameplay.Entities.Barracks
{
    public class BarrackPlace : MonoBehaviour
    {
        public UnityEvent<BarrackPlace> onPlaceBought = new();
        [SerializeField] private CurrencyStackTarget currencyStackTarget;
        public Barrack Barrack { get; private set; }
        [field: SerializeField] public Currency Price { get; private set; }

        private void Start()
        {
            currencyStackTarget.onCurrencyAdded.AddListener(OnCurrencyAdded);
            currencyStackTarget.SetTargetCount(Price);
        }

        private void OnDestroy()
        {
            currencyStackTarget.onCurrencyAdded.RemoveListener(OnCurrencyAdded);
        }

        private void OnCurrencyAdded()
        {
            if (Price.CurrencyType == currencyStackTarget.Currency.CurrencyType &&
                Price.Count <= currencyStackTarget.Currency.Count)
                onPlaceBought?.Invoke(this);
        }

        public void Build(Barrack barrack)
        {
            Barrack = barrack;
            currencyStackTarget.gameObject.SetActive(false);
        }
    }
}