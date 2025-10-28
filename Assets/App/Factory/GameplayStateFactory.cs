using App.Gameplay.GameplayStates;
using App.Gameplay.GameplayStates.States;
using VContainer;

namespace App.Factory
{
    public interface IGameplayStateFactory
    {
        T Create<T>() where T: IGameplayState;
    }
    
    public class GameplayStateFactory : IGameplayStateFactory
    {
        private readonly IObjectResolver _objectResolver;

        public GameplayStateFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public T Create<T>() where T: IGameplayState
        {
            return _objectResolver.Resolve<T>();
        }
    }
}