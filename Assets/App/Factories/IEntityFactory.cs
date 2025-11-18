using App.Gameplay.Configurations;
using App.Gameplay.Entities.Characters;
using App.Services;
using VContainer;
using VContainer.Unity;

namespace App.Factories
{
    public interface IEntityFactory
    {
        T Create<T>(EntityID entityId) where T : Character;
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

        public T Create<T>(EntityID entityId) where T: Character
        {
            var prefab = _entitiesConfiguration.Entities[entityId] as T;
            return _objectResolver.Instantiate(prefab);
        }
    }
}