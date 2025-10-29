using System;
using System.Collections.Generic;
using App.Core;
using App.Gameplay.Entities.Characters;
using App.Gameplay.Entities.Currencies;
using App.Services;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.Entities.Barracks
{
    public class Barrack: MonoBehaviour
    {
        [SerializeField] private EntityID[] entityPerLevels;
        [SerializeField] private float[] spawnDelayPerLevel;
        [SerializeField] private GameObject[] visualPerLevel;
        [SerializeField] private int[] pricePerLevel;
        
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

    public class BarrackPlace : MonoBehaviour
    {
        public UnityEvent onCurrencyAdded = new();
        public Barrack Barrack { get; private set; }
        public Currency Currency { get; private set; }

        public void AddCurrency(Currency currency)
        {
            Currency += currency;
            onCurrencyAdded?.Invoke();
        }
    }

    public enum BarrackId
    {
        
    }

    public interface IBarrackFactory
    {
        T Create<T>(BarrackId barrackId) where T : Barrack;
    }
    public class BarrackFactory: IBarrackFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly BarracksConfiguration _barracksConfiguration;
        
        public BarrackFactory(IObjectResolver objectResolver, IConfigurationService configurationService)
        {
            _objectResolver = objectResolver;
            BarracksConfiguration barracksConfiguration =
                configurationService.GetConfiguration<BarracksConfiguration>();
        }

        public T Create<T>(BarrackId barrackId) where T : Barrack
        {
            T prefab = _barracksConfiguration.Prefabs[barrackId] as T;
            return _objectResolver.Instantiate(prefab);

        }
    }

    public class BarracksConfiguration: Configuration
    {
        [field: SerializeField]
        public SerializedDictionary<BarrackId, Barrack> Prefabs { get; private set; }
    }

    public class BarrackSystem: IInitializable, ITickable
    {
        private readonly IBarrackFactory _barrackFactory;
        private List<BarrackPlace> _barrackPlaces;

        public BarrackSystem(IBarrackFactory barrackFactory, List<BarrackPlace> barrackPlaces)
        {
            _barrackFactory = barrackFactory;
            _barrackPlaces = barrackPlaces;
        }

        public void Initialize()
        {
        }

        public void Tick()
        {
            foreach (var barrackPlace in _barrackPlaces)
            {
                //if()
            }
        }
    }
}