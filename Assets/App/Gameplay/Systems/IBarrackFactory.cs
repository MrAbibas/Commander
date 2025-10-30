using App.Gameplay.Entities.Barracks;

namespace App.Gameplay.Systems
{
    public interface IBarrackFactory
    {
        T Create<T>(BuildingId buildingId) where T : Barrack;
    }
}