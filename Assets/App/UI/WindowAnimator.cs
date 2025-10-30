using System;
using UnityEngine;

namespace App.UI
{
    public class WindowAnimator : MonoBehaviour
    {
        private static readonly int _sCloseTriggerId = Animator.StringToHash("Close");
        private static readonly int _sOpenTriggerId = Animator.StringToHash("Open");
        
        public Animator animator;
        
        private Action _onCloseAnimComplete;
        private Action _onOpenAnimComplete;

        public void PlayCloseAnim(Action onComplete = null)
        {
            _onCloseAnimComplete = onComplete;
            animator?.SetTrigger(_sCloseTriggerId);
        }

        public void PlayOpenAnim(Action onComplete = null)
        {   
            _onOpenAnimComplete = onComplete;
            animator?.SetTrigger(_sOpenTriggerId);
        }

        public void PlayAnim(int triggerID)
        {
            animator?.SetTrigger(triggerID);
        }

        private void OnCloseAnimComplete() =>
            _onCloseAnimComplete?.Invoke();
        private void OnOpenAnimComplete() =>
            _onCloseAnimComplete?.Invoke();
    }
}