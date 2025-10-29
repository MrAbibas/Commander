using App.Gameplay.Entities;
using App.Gameplay.Entities.Characters.Players;

namespace App.Gameplay.Providers
{
    public interface IPlayerProvider
    {
        Player Player { get; }
        void SetPlayer(Player player);
    }
    public class PlayerProvider: IPlayerProvider
    {
        public Player Player { get; private set; }
        
        public void SetPlayer(Player player) => Player = player;
    }
}