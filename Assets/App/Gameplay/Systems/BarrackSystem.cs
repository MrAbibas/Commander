using System.Collections.Generic;
using App.Factory;
using App.Gameplay.Entities;
using App.Gameplay.Entities.Barracks;
using App.Gameplay.Entities.Characters;
using App.UI.ChooseBuildingWindow;
using App.UI.Core;
using VContainer.Unity;

namespace App.Gameplay.Systems
{
    public class BarrackSystem: IInitializable, ITickable
    {
        private List<BarrackPlace> _barrackPlaces;
        private Formation _friendlyFormation;
        private readonly IBarrackFactory _barrackFactory;
        private readonly IEntityFactory _entityFactory;
        private readonly UIFactory  _uiFactory;

        public BarrackSystem(IBarrackFactory barrackFactory,
            IEntityFactory entityFactory,
            List<BarrackPlace> barrackPlaces,
            Formation friendlyFormation,
            UIFactory uiFactory)
        {
            _barrackFactory = barrackFactory;
            _entityFactory = entityFactory;
            _barrackPlaces = barrackPlaces;
            _friendlyFormation = friendlyFormation;
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            foreach (var barrackPlace in _barrackPlaces)
            {
                barrackPlace.onPlaceBought.AddListener(OnBarrackPlaceBoughtHandler);
            }
        }

        private void OnBarrackPlaceBoughtHandler(BarrackPlace barrackPlace)
        {
            var window = _uiFactory.CreateWindow<ChooseBuildingWindow>();
            window.onBuildingChoosed.AddListener((x) => OnBuildingChooseHandler(barrackPlace, x));
        }

        private void OnBuildingChooseHandler(BarrackPlace barrackPlace, BuildingId buildingId)
        {
            var barrack = _barrackFactory.Create<Barrack>(buildingId);
            barrackPlace.Build(barrack);
        }

        public void Tick()
        {
            foreach (var barrackPlace in _barrackPlaces)
            {
                if(barrackPlace.Barrack == null) continue;
                
                var barrack = barrackPlace.Barrack;
                if(_friendlyFormation.IsFull) continue;
                if (barrack.SpawnTimer >= barrack.SpawnDelay)
                {
                    var character = _entityFactory.Create<AICharacter>(barrack.EntityId);
                    character.transform.position = barrack.SpawnPoint.position;
                    character.transform.rotation = barrack.SpawnPoint.rotation;
                    _friendlyFormation.AddCharacter(character);
                    barrack.SpawnTimer -= barrack.SpawnDelay;
                }
            }
        }
    }
}