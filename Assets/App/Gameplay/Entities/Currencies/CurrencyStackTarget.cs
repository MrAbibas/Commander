using TMPro;
using UnityEngine;

namespace App.Gameplay.Entities.Currencies
{
    public class CurrencyStackTarget : CurrencyStack
    {
        [SerializeField] private TMP_Text countText;
        public Currency Currency { get; set; }

        public void SetTargetCount(Currency currency)
        {
            Currency = new Currency() { CurrencyType = currency.CurrencyType };
            size = currency.Count;
            countText.text = (size - Currency.Count).ToString();
        }
        
        public override void AddCurrency(CurrencyCollectable newCurrency)
        {
            Currency += newCurrency.Currency;
            countText.text = (size - Currency.Count).ToString();
            newCurrency.Despawn();
        }
    }

    public class CurrencyStackSource : CurrencyStack
    {
        
    }
}