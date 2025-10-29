using App.Core;
using App.Gameplay.Entities.Characters;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.Gameplay.Configurations
{
    [CreateAssetMenu(fileName = "CombatConfiguration", menuName = "Configurations/CombatConfiguration")]
    public class CombatConfiguration: Configuration
    {
        [field: SerializeField]
        public SerializedDictionary<EntityID, float> Ranges { get; private set; }
    }
}