using App.GameStates.States;
using VContainer;

namespace App.Factory
{
    public class GameStateFactory
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