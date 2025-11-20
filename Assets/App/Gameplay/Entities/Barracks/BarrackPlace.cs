using System.Collections.Generic;
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
        [field: SerializeField] public List<BuildingId> AvailableBarracks { get; private set; }

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
            if (currencyStackTarget.Currencies.TryGetValue(Price.CurrencyType, out Currency currency) == false) return;
            if (Price.Count <= currency.Count)
                onPlaceBought?.Invoke(this);
        }

        public void PlaceBarrack(Barrack barrack)
        {
            Barrack = barrack;
            barrack.transform.SetParent(transform);
            barrack.transform.localPosition = Vector3.zero;
            barrack.transform.localRotation = Quaternion.identity;
            currencyStackTarget.gameObject.SetActive(false);
        }
    }
}