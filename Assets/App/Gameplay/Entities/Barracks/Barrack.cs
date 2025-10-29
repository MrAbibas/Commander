using System;
using App.Gameplay.Entities.Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Gameplay.Entities.Barracks
{
    public class Barrack: MonoBehaviour
    {
        [SerializeField] private EntityID[] entityPerLevels;
        [SerializeField] private float[] spawnDelayPerLevel;
        [SerializeField] private GameObject[] visualPerLevel;
        [SerializeField] private int[] pricePerLevel;
        [field: SerializeField]
        public Transform SpawnPoint { get; private set; }
        
        public EntityID EntityId => entityPerLevels[Mathf.Clamp(Level, 0, entityPerLevels.Length - 1)];
        public float SpawnDelay => spawnDelayPerLevel[Mathf.Clamp(Level, 0, spawnDelayPerLevel.Length - 1)];
        public float SpawnTimer { get; set; }
        public int Level { get; private set; }

        public void SetLevel(int level)
        {
            Level = level;
            ChangeVisual();
        }

        private void ChangeVisual()
        {
            for (int i = 0; i < visualPerLevel.Length; i++)
                visualPerLevel[i].SetActive(false);

            visualPerLevel[Mathf.Clamp(Level, 0, visualPerLevel.Length - 1)].SetActive(true);
        }

        public void Upgrade()
        {
            Level++;
            ChangeVisual();
        }
        
    }
}