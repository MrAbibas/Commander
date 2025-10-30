using UnityEngine;

namespace App.Gameplay.Entities.Currencies
{
    public class VerticalCurrencyStackSource : CurrencyStackSource
    {
        [SerializeField] private float yRotation = 0f;

        public override Vector3 GetCurrencyPosition(int ind) => Vector3.up * (ind * currencySize.y);
        public override Vector3 GetNewCurrencyPosition() => Vector3.up * ((currencies.Count + CurrenciesInTransfer.Count) * currencySize.y);
        public override Quaternion GetNewCurrencyRotation() =>
            Quaternion.Euler(0f, (currencies.Count + CurrenciesInTransfer.Count) % 2 != 0 ? yRotation : 0f, 0f);
    }
}