using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace App.UI
{
    public class ClosePanel : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent OnClick = new UnityEvent();

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick.Invoke();
        }
    }
}