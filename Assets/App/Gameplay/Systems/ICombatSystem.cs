namespace App.Gameplay.Systems
{
    public interface ICombatSystem
    {
        bool IsFighting { get; }
        void Update();
        void StartFight();
    }
}