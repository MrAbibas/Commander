using System;
using UnityEngine;

namespace App.UI.Core
{
    [RequireComponent(typeof(WindowAnimator))]
    public class BaseWindow : MonoBehaviour
    {
        protected WindowAnimator _animator;
        protected ClosePanel closePanel;
        private bool _isOpened = false;
        public bool IsOpened
        {
            get => _isOpened && isActiveAndEnabled;
            set => _isOpened = value;
        }

        public Action OnCloseComplete;

        public virtual void Init()
        {
            _animator = GetComponent<WindowAnimator>();
            closePanel = GetComponentInChildren<ClosePanel>();
            gameObject.SetActive(false);
        }
        public virtual void Open()
        {
            gameObject.SetActive(true);
            SubscribeToClosePanel();
            _animator.PlayOpenAnim(() =>
            {
            });
            IsOpened = true;
        }

        protected virtual void SubscribeToClosePanel()
        {
            closePanel?.OnClick.AddListener(() => CloseAnim());
        }

        public virtual void Close()
        {
            closePanel?.OnClick.RemoveAllListeners();
            IsOpened = false;
            OnCloseComplete?.Invoke();
            Destroy(gameObject);
        }
        public virtual void CloseAnim(Action onCloseAnimComplete = null)
        {
            if (IsOpened == false) return;
            closePanel?.OnClick.RemoveAllListeners();
            IsOpened = false;

            _animator.PlayCloseAnim(() =>
            {
                onCloseAnimComplete?.Invoke();
                Close();
            });
        }
    }
}