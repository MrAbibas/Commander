using TMPro;
using UnityEngine;

namespace App.Gameplay.Entities.Currencies
{
    public class CurrencyStackTarget : CurrencyStack
    {
        [SerializeField] private TMP_Text countText;
        [field: SerializeField] public CurrencyType TargetCurrencyType { get; private set; }

        public void SetTargetCount(Currency currency)
        {
            TargetCurrencyType = currency.CurrencyType;
            Currency = new() { [currency.CurrencyType] = new Currency()
            {
                CurrencyType = currency.CurrencyType,
                Count = 0
            } };
            size = currency.Count;
            countText.text = (size - Currency[TargetCurrencyType].Count).ToString();
        }

        public override void AddCurrencyCollectible(CurrencyCollectable newCurrency)
        {
            base.AddCurrencyCollectible(newCurrency);
            countText.text = (size - Currency[TargetCurrencyType].Count).ToString();
            newCurrency.Despawn();
        }
    }
}