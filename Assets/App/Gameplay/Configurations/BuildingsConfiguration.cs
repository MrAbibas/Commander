using App.Core;
using App.Gameplay.Entities.Barracks;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.Gameplay.Configurations
{
    [CreateAssetMenu(fileName = "BuildingsConfiguration", menuName = "Configurations/BuildingsConfiguration")]
    public class BuildingsConfiguration: Configuration
    {
        [field: SerializeField]
        public SerializedDictionary<BuildingId, Barrack> Prefabs { get; private set; }
    }
}