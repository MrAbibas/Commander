using System;
using App.Gameplay.Entities.Currencies;
using UnityEngine;

namespace App.Gameplay.Entities.Characters.Components
{
    public class CurrencyDispatcher: MonoBehaviour
    {
        [SerializeField]
        private CurrencyStack currencyStack;
        [SerializeField] private float delay;
        private float _delayTimer;

        private void Update()
        {
            _delayTimer += Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if(_delayTimer < delay) return;
            HandleCollectables(other);
            HandleStacks(other);
            if(currencyStack.IsEmpty) return;
            if (other.TryGetComponent(out CurrencyStack otherStack))
            {
                
            }
        }

        private void HandleStacks(Collider other)
        {
            if (other.TryGetComponent(out CurrencyStack stack))
            {
                if (stack is CurrencyStackSource source && stack.IsEmpty == false && currencyStack.IsFool == false)
                    source.TransferLastToOtherStack(currencyStack);
                else if (stack is CurrencyStackTarget target && stack.IsFool == false &&
                         currencyStack.IsEmpty == false)
                {
                    currencyStack.TransferLastToOtherStack(target, target.Currency.CurrencyType);
                }
            }
        }

        private void HandleCollectables(Collider other)
        {
            if(currencyStack.IsFool) return;
            if (other.TryGetComponent(out CurrencyCollectable collectable))
            {
                currencyStack.TransferToStack(collectable);
                _delayTimer = 0;
            }
        }
    }
}