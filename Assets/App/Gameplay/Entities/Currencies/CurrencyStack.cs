using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace App.Gameplay.Entities.Currencies
{
    public class CurrencyStack : MonoBehaviour
    {
        [SerializeField]
        protected List<CurrencyCollectable> currencies = new();
        [SerializeField]
        protected Vector3 currencySize = new Vector3(0.8f, 0.1f, 0.4f);
        [SerializeField]
        private float jumpPower = 2f;
        [SerializeField]
        private float moveDuration = 0.8f;
        [SerializeField] private int size;
        public List<CurrencyCollectable> CurrenciesInTransfer { get; protected set; } = new();
        public bool IsFool => currencies.Count + CurrenciesInTransfer.Count >= size;
        
        private void Start()
        {
            currencies = GetComponentsInChildren<CurrencyCollectable>().ToList();
        }

        public void WaitTransfer(CurrencyCollectable collectable)
        {
            CurrenciesInTransfer.Add(collectable);
        }
        
        public virtual void AddCurrency(CurrencyCollectable newCurrency)
        {
            CurrenciesInTransfer.Remove(newCurrency);
            newCurrency.transform.SetParent(transform);
            newCurrency.transform.position = GetCurrencyPosition();
            newCurrency.transform.rotation = GetCurrencyRotation();
            currencies.Add(newCurrency);
        }

        public virtual Vector3 GetCurrencyPosition() => transform.position;
        public virtual Quaternion GetCurrencyRotation() => transform.rotation;

        public virtual Sequence GetTransferSequence(CurrencyCollectable currency, CurrencyStack otherStack)
        {
            Sequence moveSequence = DOTween.Sequence();
            
            moveSequence.Append(currency.transform.DOScale(1.2f, 0.2f));
            moveSequence.Append(currency.transform.DOJump(
                otherStack.GetCurrencyPosition(), 
                jumpPower, 
                1, 
                moveDuration
            ).SetEase(Ease.OutQuad));
            
            moveSequence.Join(currency.transform.DORotate(otherStack.GetCurrencyRotation().eulerAngles, 0.2f));
            moveSequence.Join(currency.transform.DOScale(1f, 0.3f).SetDelay(moveDuration - 0.3f));
            
            moveSequence.OnComplete(() => otherStack.AddCurrency(currency));
            return moveSequence;
        }

        public void TransferToOtherStack(CurrencyCollectable collectable, CurrencyStack otherStack)
        {
            currencies.Remove(collectable);
            collectable.transform.SetParent(null);
            Sequence moveSequence = GetTransferSequence(collectable, otherStack);
            moveSequence.Play();
        }
    }
}