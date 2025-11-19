using System;
using System.Collections.Generic;
using App.Gameplay.Entities.Barracks;
using App.UI.Core;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Events;

namespace App.UI.ChooseBuildingWindow
{
    public class ChooseBuildingWindow : BaseWindow
    {
        [Serializable]
        public class BuildingViewConfiguration
        {
            public Sprite icon;
            public string title;
        }
        public UnityEvent<BuildingId> onBuildingChoosed;
        [SerializeField] private RectTransform _content;
        [SerializeField] private BuildingView _viewPrefab;
        [SerializeField] private SerializedDictionary<BuildingId, BuildingViewConfiguration> configs;
        
        protected override void SubscribeToClosePanel()
        {
        }

        public void Open(List<BuildingId> buildings)
        {
            base.Open();
            foreach (var buildingId in buildings)
            {
                var building = Instantiate(_viewPrefab, _content);
                var config = configs[buildingId];
                building.Init(buildingId, config.icon, config.title);
                building.OnClick.AddListener(OnBuildingSelected);
            }
        }

        private void OnBuildingSelected(BuildingId buildingId)
        {
            if(IsOpened == false) return;
            onBuildingChoosed?.Invoke(buildingId);
        }
    }
}
