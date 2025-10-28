using App.Gameplay.Configurations;
using App.Gameplay.Entities;
using App.GameStates.States;
using App.Services;
using VContainer;
using VContainer.Unity;

namespace App.Factory
{
    public interface IEntityFactory
    {
        T Create<T>(EntityId entityId) where T : Entity;
    }

    public class EntityFactory : IEntityFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly EntitiesConfiguration _entitiesConfiguration;
        
        public EntityFactory(IObjectResolver objectResolver, IConfigurationService configurationService)
        {
            _objectResolver = objectResolver;
            _entitiesConfiguration = configurationService.GetConfiguration<EntitiesConfiguration>();
        }

        public T Create<T>(EntityId entityId) where T: Entity
        {
            var prefab = _entitiesConfiguration.Entities[entityId] as T;
            return _objectResolver.Instantiate(prefab);
        }
    }
}