using UnityEngine;

namespace App.Gameplay.Entities.Currencies
{
    public class VerticalCurrencyStack : CurrencyStack
    {
        [SerializeField] private float yRotation = 0f;

        public override Vector3 GetCurrencyPosition() => transform.position +  Vector3.up * currencySize.y;

        override public Quaternion GetCurrencyRotation() =>
            transform.rotation * Quaternion.Euler(0f, currencies.Count % 2 != 0 ? yRotation : 0f, 0f);
    }
}