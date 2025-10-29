using App.Core;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using EntityId = App.Gameplay.Entities.Characters.EntityId;

namespace App.Gameplay.Configurations
{
    [CreateAssetMenu(fileName = "CombatConfiguration", menuName = "Configurations/CombatConfiguration")]
    public class CombatConfiguration: Configuration
    {
        [field: SerializeField]
        public SerializedDictionary<EntityId, float> Ranges { get; private set; }
    }
}