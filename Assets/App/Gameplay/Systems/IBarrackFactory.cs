using App.Gameplay.Entities.Barracks;

namespace App.Gameplay.Systems
{
    public interface IBarrackFactory
    {
        T Create<T>(BarrackId barrackId) where T : Barrack;
    }
}