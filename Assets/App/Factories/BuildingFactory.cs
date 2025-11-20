using App.Gameplay.Configurations;
using App.Gameplay.Entities.Barracks;
using App.Gameplay.Systems;
using App.Services;
using VContainer;
using VContainer.Unity;

namespace App.Factories
{
    public class BuildingFactory: IBarrackFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly BuildingsConfiguration _buildingsConfiguration;
        
        public BuildingFactory(IObjectResolver objectResolver, IConfigurationService configurationService)
        {
            _objectResolver = objectResolver;
            _buildingsConfiguration =
                configurationService.GetConfiguration<BuildingsConfiguration>();
        }

        public T Create<T>(BuildingId buildingId) where T : Barrack
        {
            T prefab = _buildingsConfiguration.Prefabs[buildingId] as T;
            return _objectResolver.Instantiate(prefab);

        }
    }
}