using App.Core;
using App.Gameplay.Entities.Barracks;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.Gameplay.Configurations
{
    public class BuildingsConfiguration: Configuration
    {
        [field: SerializeField]
        public SerializedDictionary<BuildingId, Barrack> Prefabs { get; private set; }
    }
}