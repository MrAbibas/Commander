using System.Collections.Generic;
using System.Linq;
using App.Gameplay.Entities.Currencies;
using UnityEngine;

namespace App.Gameplay.Entities.Characters.Components
{
    public class CurrencyContainer: MonoBehaviour
    {
        private Dictionary<CurrencyType, Currency> _currencies = new();
        private int _maxCount;
        public bool IsFool => _currencies.Sum(x => x.Value.Count) >= _maxCount;

        public bool TryGetCurrency(Currency currency)
        {
            if (ContainsCurrency(currency.CurrencyType, out Currency value, currency.Count))
            {
                value -= currency;
                return true;
            }
            return false;
        }

        public bool ContainsCurrency(CurrencyType currencyType, out Currency value, int count = 1)
        {
            if(_currencies.TryGetValue(currencyType, out value) == false) return false;
            if(value.Count < count) return false;
            return true;
        }

        public void AddCurrency(Currency currency)
        {
            if(_currencies.TryGetValue(currency.CurrencyType, out Currency value))
                value += currency;
            else
                _currencies.Add(currency.CurrencyType, currency);
        }
    }
}