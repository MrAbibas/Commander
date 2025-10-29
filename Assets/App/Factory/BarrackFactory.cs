using App.Gameplay.Configurations;
using App.Gameplay.Entities.Barracks;
using App.Gameplay.Systems;
using App.Services;
using VContainer;
using VContainer.Unity;

namespace App.Factory
{
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
}