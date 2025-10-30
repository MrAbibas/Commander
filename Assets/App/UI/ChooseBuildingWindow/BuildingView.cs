using App.Gameplay.Entities.Barracks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.UI.ChooseBuildingWindow
{
    public class BuildingView: MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent<BuildingId> OnClick;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text text;
        private BuildingId _buildingId;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_buildingId);
        }

        public void Init(BuildingId buildingId, Sprite icon, string text)
        {
            _buildingId = buildingId;
            this.icon.sprite = icon;
            this.text.text = text;
        }
    }
}