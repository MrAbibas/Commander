using App.Core;
using App.Gameplay.Entities;
using App.Gameplay.Entities.Characters;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.Gameplay.Configurations
{
    [CreateAssetMenu(fileName = "EntityConfiguration", menuName = "Configurations/EntityConfiguration", order = 0)]
    public class EntitiesConfiguration: Configuration
    {
        [field: SerializeField]
        public SerializedDictionary<EntityID, Character> Entities { get; private set; }
    }
}