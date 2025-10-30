using System;
using DG.Tweening;
using UnityEngine;

namespace App.UI.Core
{
    public class WindowAnimator : MonoBehaviour
    {
        [Header("Animation Settings")] [SerializeField]
        private RectTransform windowTransform;

        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Open Animation")] [SerializeField]
        private float openDuration = 0.3f;

        [SerializeField] private Ease openEase = Ease.OutBack;
        [SerializeField] private Vector3 openScale = Vector3.one;

        [Header("Close Animation")] [SerializeField]
        private float closeDuration = 0.2f;

        [SerializeField] private Ease closeEase = Ease.InBack;
        [SerializeField] private Vector3 closeScale = Vector3.zero;

        [Header("Fade Settings")] [SerializeField]
        private bool useFade = true;

        [SerializeField] private float fadeDuration = 0.2f;

        private Sequence _currentSequence;

        private void Awake()
        {
            if (windowTransform == null)
                windowTransform = GetComponent<RectTransform>();

            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
        }

        public void PlayCloseAnim(Action onComplete = null)
        {
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();

            if (useFade && canvasGroup != null)
            {
                _currentSequence.Append(DOTween.To(
                    () => canvasGroup.alpha,
                    x => canvasGroup.alpha = x,
                    0f,
                    fadeDuration)
                    .SetEase(closeEase));
                _currentSequence.Join(windowTransform.DOScale(closeScale, closeDuration).SetEase(closeEase));
            }
            else
            {
                _currentSequence.Append(windowTransform.DOScale(closeScale, closeDuration).SetEase(closeEase));
            }

            _currentSequence.OnComplete(() =>
            {
                gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }

        public void PlayOpenAnim(Action onComplete = null)
        {
            gameObject.SetActive(true);
            _currentSequence?.Kill();
            
            windowTransform.localScale = closeScale;
            if (canvasGroup != null)
                canvasGroup.alpha = useFade ? 0f : 1f;

            _currentSequence = DOTween.Sequence();

            if (useFade && canvasGroup != null)
            {
                _currentSequence.Append(DOTween.To(
                    () => canvasGroup.alpha,
                    x => canvasGroup.alpha = x,
                    1f,
                    fadeDuration)
                    .SetEase(openEase));
                _currentSequence.Join(windowTransform.DOScale(openScale, openDuration).SetEase(openEase));
            }
            else
            {
                _currentSequence.Append(windowTransform.DOScale(openScale, openDuration).SetEase(openEase));
            }

            _currentSequence.OnComplete(() => onComplete?.Invoke());
        }

        private void OnDestroy()
        {
            _currentSequence?.Kill();
        }
    }
}