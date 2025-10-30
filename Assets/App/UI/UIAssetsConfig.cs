using App.Core.Attributes;
using App.Utils;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace App.UI
{
    [CreateAssetMenu(fileName = "UIAssetsConfig", menuName = "Game/UIAssetsConfig", order = 0)]
    public class UIAssetsConfig : ScriptableObject
    {
        public GameObject uiRoot;
        
        [SerializedDictionary(keyName: "Type", valueName: "Prefab"), TypeFilter(typeof(BaseWindow))]
        public SerializedDictionary<SerializableType, BaseWindow> Windows;
    }
}