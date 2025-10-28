using App.Core.Attributes;
using App.Utils;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.Core
{
    [CreateAssetMenu(fileName = "Configurations", menuName = "Configurations/Configurations")]
    public class Configurations: ScriptableObject
    {
        [TypeFilter(typeof(Configuration))] 
        public SerializedDictionary<SerializableType, Configuration> allConfigurations;
    }
}