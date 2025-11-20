using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace App.Gameplay.Entities.Currencies
{
    public class CurrencyStack : MonoBehaviour
    {
        public UnityEvent onCurrencyAdded = new ();
        public UnityEvent onCurrencyRemoved  = new ();
        
        public Dictionary<CurrencyType,Currency> Currencies { get; protected set; }
        [SerializeField]
        protected List<CurrencyCollectable> currencyCollectables = new();
        [SerializeField]
        protected Vector3 currencySize = new Vector3(0.8f, 0.1f, 0.4f);
        [SerializeField] protected int size;
        
        [Header("TransferAnimation")]
        [SerializeField] private float jumpPower = 2f;
        [SerializeField] private float moveDuration = 0.8f;
        [SerializeField] private float scaleDuration = 0.1f;
        [SerializeField] private float maxScale = 1.3f;
        public List<CurrencyCollectable> CurrenciesInTransfer { get; protected set; } = new();
        public bool IsFool => currencyCollectables.Count + CurrenciesInTransfer.Count >= size;
        public bool IsEmpty => currencyCollectables.Count == 0;
        
        private void Start()
        {
            currencyCollectables = GetComponentsInChildren<CurrencyCollectable>().ToList();
            Currencies = new ();
            foreach (var collectable in currencyCollectables)
                AddCurrency(collectable.Currency);
        }

        public void WaitTransfer(CurrencyCollectable collectable)
        {
            CurrenciesInTransfer.Add(collectable);
        }

        public void AddCurrency(Currency currency)
        {
            if (Currencies.TryGetValue(currency.CurrencyType, out Currency currencyValue))
                currencyValue += currency;
            else
                Currencies.Add(currency.CurrencyType,
                    new Currency() { CurrencyType = currency.CurrencyType, Count = currency.Count });
            
            onCurrencyAdded.Invoke();
        }

        public void RemoveCurrency(Currency currency)
        {
            if (Currencies.TryGetValue(currency.CurrencyType, out Currency currencyValue) == false) return;

            currencyValue -= currency;
            onCurrencyRemoved.Invoke();
        }

        public virtual void AddCurrencyCollectible(CurrencyCollectable newCurrency)
        {
            CurrenciesInTransfer.Remove(newCurrency);
            if (newCurrency.transform.parent != transform)
                newCurrency.transform.SetParent(transform, true);
            
            currencyCollectables.Add(newCurrency);
            AddCurrency(newCurrency.Currency);
        }

        public virtual void RemoveCurrencyCollectible(CurrencyCollectable currency)
        {
            currencyCollectables.Remove(currency);
            for(int i = 0; i < currencyCollectables.Count; i++)
                currencyCollectables[i].transform.localPosition = GetCurrencyPosition(i);

            RemoveCurrency(currency.Currency);
        }

        public virtual Vector3 GetNewCurrencyPosition() => Vector3.zero;
        public virtual Vector3 GetCurrencyPosition(int ind) => Vector3.zero;
        public virtual Quaternion GetNewCurrencyRotation() => Quaternion.identity;

        public virtual Sequence GetTransferSequence(CurrencyCollectable currency, CurrencyStack targetStack)
        {
            Sequence moveSequence = DOTween.Sequence();
            Vector3 scale = currency.transform.localScale;
            moveSequence.Append(currency.transform.DOScale(scale * maxScale, scaleDuration));
            moveSequence.AppendCallback(() => currency.transform.SetParent(targetStack.transform, true));
            var endPos = targetStack.GetNewCurrencyPosition();
            moveSequence.Append(currency.transform.DOLocalJump(
                endPos, 
                jumpPower, 
                1, 
                moveDuration
            ).SetEase(Ease.OutExpo));
            
            moveSequence.Join(currency.transform.DOLocalRotate(targetStack.GetNewCurrencyRotation().eulerAngles, moveDuration/2f).SetDelay(scaleDuration));
            moveSequence.Join(currency.transform.DOScale(scale, moveDuration - scaleDuration).SetDelay(scaleDuration));
            
            moveSequence.OnComplete(() => targetStack.AddCurrencyCollectible(currency));
            return moveSequence;
        }

        public void TransferToOtherStack(CurrencyCollectable collectable, CurrencyStack otherStack)
        {
            otherStack.WaitTransfer(collectable);
            RemoveCurrencyCollectible(collectable);
            collectable.transform.SetParent(null);
            Sequence moveSequence = GetTransferSequence(collectable, otherStack);
            moveSequence.Play();
        }

        public void TransferLastToOtherStack(CurrencyStack otherStack)
        {
            TransferToOtherStack(currencyCollectables[^1], otherStack);
        }

        public void TransferLastToOtherStack(CurrencyStack otherStack, CurrencyType type)
        {
            var currency = currencyCollectables.FindLast((x) => x.Currency.CurrencyType == type);
            if (currency != null)
                TransferToOtherStack(currencyCollectables[^1], otherStack);
        }

        public void TransferToStack(CurrencyCollectable collectable)
        {
            collectable.transform.SetParent(null);
            CurrenciesInTransfer.Add(collectable);
            Sequence moveSequence = GetTransferSequence(collectable, this);
            moveSequence.Play();
        }
    }
}