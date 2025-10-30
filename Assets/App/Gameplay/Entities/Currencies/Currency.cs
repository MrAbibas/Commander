using System;

namespace App.Gameplay.Entities.Currencies
{
    [Serializable]
    public class Currency
    {
        public int Count;
        public CurrencyType CurrencyType;
        
        public static Currency operator+(Currency valueA, Currency valueB)
        {
            valueA.Count += valueB.Count;
            return valueA;
        }
        public static Currency operator-(Currency valueA, Currency valueB)
        {
            valueA.Count -= valueB.Count;
            return valueA;
        }
    }
}