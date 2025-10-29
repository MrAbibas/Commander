using System.Collections.Generic;
using App.Factory;
using App.Gameplay.Entities;
using App.Gameplay.Entities.Barracks;
using App.Gameplay.Entities.Characters;
using VContainer.Unity;

namespace App.Gameplay.Systems
{
    public class BarrackSystem: IInitializable, ITickable
    {
        private readonly IBarrackFactory _barrackFactory;
        private readonly IEntityFactory _entityFactory;
        private List<BarrackPlace> _barrackPlaces;
        private Formation _friendlyFormation;

        public BarrackSystem(IBarrackFactory barrackFactory, IEntityFactory entityFactory ,List<BarrackPlace> barrackPlaces, Formation friendlyFormation)
        {
            _barrackFactory = barrackFactory;
            _entityFactory = entityFactory;
            _barrackPlaces = barrackPlaces;
            _friendlyFormation = friendlyFormation;
        }

        public void Initialize()
        {
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