using UnityEngine;

namespace App.Gameplay.Entities.Currencies
{
    public class CurrencyCollectable : MonoBehaviour
    {
        [field: SerializeField] public Currency Currency { get; private set; }
    }
}