using App.Gameplay.GameplayStates;
using App.GameStates.States;
using VContainer;

namespace App.Factory
{
    public interface IGameStateFactory
    {
        T Create<T>() where T: IGameState;
    }

    public class GameStateFactory : IGameStateFactory
    {
        private readonly IObjectResolver _objectResolver;

        public GameStateFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public T Create<T>() where T: IGameState
        {
            return _objectResolver.Resolve<T>();
        }
    }
}