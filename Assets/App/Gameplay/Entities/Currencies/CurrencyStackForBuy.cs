using TMPro;
using UnityEngine;

namespace App.Gameplay.Entities.Currencies
{
    public class CurrencyStackForBuy : CurrencyStack
    {
        [SerializeField] private TMP_Text countText;
        public Currency Currency { get; set; }

        public void SetTargetCount(int targetCount)
        {
            size = targetCount;
        }
        
        public override void AddCurrency(CurrencyCollectable newCurrency)
        {
            Currency += newCurrency.Currency;
            countText.text = (size - Currency.Count).ToString();
            newCurrency.Despawn();
        }
    }
}